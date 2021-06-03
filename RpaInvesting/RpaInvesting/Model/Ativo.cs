namespace RpaInvesting.Model
{
    class Ativo
    {
        public string nomeAtivo { get; set; }
        public string nomeEmpresa { get; set; }
        public string diaExecucao { get; set; }
        public double preco { get; set; }
        public string isProcessed { get; set; }

        public Ativo()
        {

        }
        public Ativo(string nomeAtivo, string nomeEmpresa, string diaExecucao, double preco, string isProcessed)
        {
            this.nomeAtivo = nomeAtivo;
            this.nomeEmpresa = nomeEmpresa;
            this.diaExecucao = diaExecucao;
            this.preco = preco;
            this.isProcessed = isProcessed;
        }
    }
}
