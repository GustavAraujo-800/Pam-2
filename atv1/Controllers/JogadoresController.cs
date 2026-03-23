using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using atv1.Models;
using atv1.Models.Enums;
using Microsoft.AspNetCore.Mvc;


namespace atv1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JogadoresController : ControllerBase
    {
        private static List<Jogador> listaJogadores = new List<Jogador>()
        {
            new Jogador(){ Id=1, Nome="Hugo Souza", NumeroCamisa=1, Posicao="Goleiro", Status=StatusJogador.Titular },
            new Jogador(){ Id=2, Nome="Yuri Alberto",NumeroCamisa=9,Posicao="Atacante",Status=StatusJogador.Titular },
            new Jogador(){ Id=3, Nome="Cássio Ramos", NumeroCamisa=12, Posicao="Goleiro", Status=StatusJogador.Reserva },
            new Jogador(){ Id=4, Nome="Fagner", NumeroCamisa=23, Posicao="Lateral Direito", Status=StatusJogador.Titular },
            new Jogador(){ Id=5, Nome="Félix Torres", NumeroCamisa=3, Posicao="Zagueiro", Status=StatusJogador.Titular },
            new Jogador(){ Id=6, Nome="Gustavo Henrique", NumeroCamisa=13, Posicao="Zagueiro", Status=StatusJogador.Reserva },
            new Jogador(){ Id=7, Nome="Matheus Bidu", NumeroCamisa=21, Posicao="Lateral Esquerdo", Status=StatusJogador.Titular },
            new Jogador(){ Id=8, Nome="Raniele", NumeroCamisa=14, Posicao="Volante", Status=StatusJogador.Titular },
            new Jogador(){ Id=9, Nome="Maycon", NumeroCamisa=7, Posicao="Meio Campo", Status=StatusJogador.Reserva },
            new Jogador(){ Id=10, Nome="Rodrigo Garro", NumeroCamisa=10, Posicao="Meio Campo", Status=StatusJogador.Titular },
            new Jogador(){ Id=11, Nome="Wesley", NumeroCamisa=36, Posicao="Atacante", Status=StatusJogador.Reserva },
            new Jogador(){ Id=12, Nome="Romero", NumeroCamisa=11, Posicao="Atacante", Status=StatusJogador.Titular },
            new Jogador(){ Id=13, Nome="Pedro Raul", NumeroCamisa=20, Posicao="Atacante", Status=StatusJogador.Reserva }
        };

        [HttpGet("GetAll")]
        public IActionResult ObterJogadores()
        {
            List<Jogador> lista = listaJogadores;
            return Ok(lista);
        }

        [HttpGet("Get")]
        public IActionResult Getfirst()
        {
            return Ok(listaJogadores[0]);    
        }

        [HttpPost]
        public IActionResult InserirJogador(Jogador j)
        {
            listaJogadores.Add(j);
            return Ok(listaJogadores);
        }

        [HttpGet("GetOrdenado")]
        public IActionResult GetOrdem()
        {
            List<Jogador> listaFinal = listaJogadores.OrderBy(j => j.Nome).ToList();
            return Ok(listaFinal);
        }

        [HttpPut("AtualizarJogador")]
        public IActionResult Atualizar(Jogador j)
        {
            Jogador jEncontrado = listaJogadores.Find(jBusca => jBusca.Id == j.Id);

            if(jEncontrado == null)
                return BadRequest("Jogador não encontrado");

            jEncontrado.Nome = j.Nome;
            jEncontrado.NumeroCamisa = j.NumeroCamisa;

            return Ok(listaJogadores);
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            listaJogadores.RemoveAll(j => j.Id == id);
            return Ok(listaJogadores);
        }

        [HttpGet("GetByNomeAproximado/{nome}")]
        public IActionResult GetByNomeAproximado(string nome)
        {
            List<Jogador> lista = listaJogadores.FindAll(j => j.Nome.ToLower().Contains(nome.ToLower()));
            return Ok(lista);
        }
    }
}