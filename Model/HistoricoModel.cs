using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace mvpAPI.Dtos
{
    public class HistoricoModel
    {
        [Key]
        public int Id { get; set; }  // Inclua uma chave primária
        
        public string Usuario { get; set; }
        
        public string DataAtual { get; set; }
        
        public string Tipo { get; set; }
        
        public string Documento { get; set; }
        
        public string Data { get; set; }
        
        public string Periodo { get; set; }
        
        public string Dados { get; set; }
        
        

       
    }
}