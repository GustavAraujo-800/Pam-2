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
    public class JogadoresExercicioController : ControllerBase
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

        //Métodos construidos aqui
         // A)
        [HttpGet("GetByName/{nome}")]
        public IActionResult GetByNome(string nome)
        {
            Jogador ProcuraNome = listaJogadores.FirstOrDefault(j => j.Nome.ToLower() == nome.ToLower());

            if(ProcuraNome == null)
                return NotFound("Jogador não encontrado.");
            return Ok(ProcuraNome);
        }

        // B)
        [HttpGet("GetTitulares")]
        public IActionResult GetTitulares(int Status)
        {
            listaJogadores.RemoveAll(j => j.Status != StatusJogador.Titular);
            listaJogadores = listaJogadores.OrderByDescending(j => j.NumeroCamisa).ToList();
            return Ok(listaJogadores);
        }
        // C)
        [HttpGet("GetEstatisticas")]
        public IActionResult GetEstatisticas()
        {
            string  totalJogadores = "Total Jogadores:" + listaJogadores.Count;
            string totalCamisas = "Total Número Camisas: " + listaJogadores.Sum(j => j.NumeroCamisa);

            return Ok(totalJogadores + totalCamisas);
        }
        // D)
        [HttpPost("PostValidacao")]
        public IActionResult PostValidacao(Jogador jogador)
        {
            if (jogador.NumeroCamisa > 100)
                return BadRequest("Número da camisa não pode ser maior que 100.");

            listaJogadores.Add(jogador);
            return Ok(jogador);
        }
        // E)
        [HttpPost("PostValidacaoNome")]
        public IActionResult PostvalidacaoNome(Jogador jogador)
        {
            if (jogador.Posicao !="Goleiro" && jogador.NumeroCamisa == 1)
                return BadRequest("Não é permitido que o camisa 1 seja goleiro");

            listaJogadores.Add(jogador);
            return Ok(jogador);
        }
        // F)

        [HttpGet("GetByStatus")]
        public IActionResult GetByStatus(StatusJogador status)
        {
            if (!Enum.IsDefined(typeof(StatusJogador), status) || status == StatusJogador.Nenhum)
                return BadRequest("Status Inválido.");
            
            var jogadoresFiltados = listaJogadores.Where(j => j.Status == status).ToList();

            if (!jogadoresFiltados.Any())
                return NotFound("Nenhum jogador encontrado com esse status.");

            return Ok(jogadoresFiltados);
        }
    }

}