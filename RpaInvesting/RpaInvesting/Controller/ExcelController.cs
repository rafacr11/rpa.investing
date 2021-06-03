using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using RpaInvesting.Constants;
using RpaInvesting.Model;
using RpaInvesting.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace RpaInvesting.Controller
{
    class ExcelController
    {
        private readonly Logging log;

        public ExcelController(Logging log)
        {
            this.log = log;
        }

        public List<Ativo> ReadExcel()
        {
            List<Ativo> ativos = new List<Ativo>();
            string filePath = InvestingConstants.EXCEL_INPUT_PATH;
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                IWorkbook book;
                book = new XSSFWorkbook(fs);

                ISheet sheet = book.GetSheetAt(0);

                foreach (IRow curRow in sheet)
                {
                    String firstCell = curRow.GetCell(0).StringCellValue;
                    if(!firstCell.Equals("") && !firstCell.Equals("Ticker"))
                    {
                        sheet.GetRow(curRow.RowNum);

                        Ativo ativo = new Ativo();

                        ativo.nomeAtivo = curRow.GetCell(0).StringCellValue.Trim();

                        ativos.Add(ativo);
                        
                    }
                }

                log.Info("Leitura de dados excel terminada");
                book.Close();


            }
            catch (Exception e)
            {
                log.Error("Falha ao ler Excel: " + e.Message);
            }

            return ativos;

        }

        public void OutputExcel(List<Ativo> ativos)
        {
            try
            {
                IWorkbook OutPutBook;
                OutPutBook = new XSSFWorkbook();

                ISheet outputsheet = OutPutBook.CreateSheet();
                int rowIndex = 1;

                IRow headersRow = outputsheet.CreateRow(0);
                headersRow.CreateCell(0).SetCellValue("Ticker");
                headersRow.CreateCell(1).SetCellValue("Empresa");
                headersRow.CreateCell(2).SetCellValue("Data");
                headersRow.CreateCell(3).SetCellValue("Preço");
                headersRow.CreateCell(4).SetCellValue("Resultado");

                foreach (Ativo ativo in ativos)
                {
                    IRow row = outputsheet.CreateRow(rowIndex++);
                    row.CreateCell(0).SetCellValue(ativo.nomeAtivo);
                    row.CreateCell(1).SetCellValue(ativo.nomeEmpresa);
                    row.CreateCell(2).SetCellValue(ativo.diaExecucao);
                    row.CreateCell(3).SetCellValue("R$  " + ativo.preco);
                    row.CreateCell(4).SetCellValue(ativo.isProcessed);
                    
                }

                
                string folderPath = @"C:\Arquivos\RpaInvesting\saida";

                if (Directory.Exists(folderPath))
                {
                    using (FileStream arquivo = File.Create(@"C:\Arquivos\RpaInvesting\saida\ativos_saida -" + DateTime.Now.ToString("dd_MM_yyyy") + ".xlsx"))
                    {                        
                        OutPutBook.Write(arquivo);
                        OutPutBook.Close();
                        log.Info("Arquivo de saída criado com sucesso");
                    }
                }
                else
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(@"C:\Arquivos\RpaInvesting\saida\ativos_saida -"));
                    using (FileStream arquivo = File.Create(@"C:\Arquivos\RpaInvesting\saida\ativos_saida -" + DateTime.Now.ToString("dd_MM_yyyy") + ".xlsx"))
                    {                        
                        OutPutBook.Write(arquivo);
                        OutPutBook.Close();
                        log.Info("Pasta de saída e arquivo criados com sucessso");
                    }
                }
               

            }
            catch(Exception e)
            {
                log.Error("Erro ao exportar arquivo" + e.Message);
            }
        }
    }
}
