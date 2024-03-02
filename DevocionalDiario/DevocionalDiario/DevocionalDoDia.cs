namespace DevocionalDiario
{
    public class DevocionalDoDia
    {
        public int Id { get; set; }

        public string Leitura { get; set; }

        public string Resumo { get; set; }
        
        public string Licao { get; set; }

        public string Versiculo { get; set; }

        public DateTime Dia { get; set; }
        
        public DevocionalDoDia()
        {
            Dia = DateTime.Now.Date;
        }
    }
}