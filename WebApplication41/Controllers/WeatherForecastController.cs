using Microsoft.AspNetCore.Mvc;
using WebApplication41.Models;
using System;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication41.Containers;

namespace WebApplication41.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        Checker checker;
        Saver saver;
        public WeatherForecastController(Checker checker , Saver saver)
        {
            this.checker = checker;
          
            this.saver = saver;
        }

        [HttpPost]
        public async Task<IActionResult> NewGame()
        {
            IDictionary<int , IEnumerable<string>>? board= await this.saver.GetAll("user.json");
            if (board is null) return BadRequest("Ошибка");
            int numb = await this.saver.Add("user.json", board,  BoardChecker.clearfields, this.saver.GetNumber(board));

            return Ok(new { number = numb, Winner = this.checker.Winner(BoardChecker.clearfields), PositionsOfWinner = this.checker.WinningScenario(BoardChecker.clearfields), gameBoard = BoardChecker.clearfields, PlayerSymbol = BoardChecker.X });
        }
        [HttpPatch]
        public async Task<IActionResult> ChangeField(IEnumerable<string> board, int number)
        {
            Status status = this.checker.CheckValidBoar(board);
            if (status.Code != 200) return  BadRequest(status.Message) ;
            IDictionary<int, IEnumerable<string>>? data = await this.saver.GetAll("user.json");
            if (data is null) return BadRequest("Ошибка");
            int numb = await this.saver.Change("user.json", data, board, number);
            return Ok(new 
                {
                    PlayerSymbol = this.checker.WhichPlayersTurn(board),
                    Winner = this.checker.Winner(board),
                    number = numb, 
                    gameBoard = board,
                    PositionsOfWinner = this.checker.WinningScenario(board)
                });
            
            
        }
        [HttpGet]
        public async Task<IActionResult> GetGame(int number)
        {
            IDictionary<int, IEnumerable<string>>? all = await this.saver.GetAll("user.json");
            if (all is null) return BadRequest("Ошибка");
            IEnumerable<string>? board = this.saver.GetGame(all, number);
            if (board is null) return BadRequest("Ошибка, такой игры нет");
            return Ok(new
            {
                PlayerSymbol = this.checker.WhichPlayersTurn(board),
                Winner = this.checker.Winner(board),
                number = number,
                gameBoard = board,
                PositionsOfWinner = this.checker.WinningScenario(board)
            });


        }
        [HttpPut]
        public async Task<IActionResult> RandomChange(IEnumerable<string> board, int number)
        {
            Status status = this.checker.CheckValidBoar(board);
            if (status.Code != 200) return BadRequest(status.Message);
            string azurePlayerSymbol = this.checker.WhichPlayersTurn(board);
            board = this.checker.PutRandomField(board, azurePlayerSymbol);
            IDictionary<int, IEnumerable<string>>? data = await this.saver.GetAll("user.json");
            if (data is null) return BadRequest("Ошибка");
            int numb = await this.saver.Change("user.json", data, board, number);
            return  Ok( new
            {
                PlayerSymbol = azurePlayerSymbol,
                Winner = this.checker.Winner(board),
                number = numb,
                gameBoard = board,
                PositionsOfWinner = this.checker.WinningScenario(board)
            });


        }
        [HttpDelete]
        public async Task<IActionResult> Delete( int number)
        {
            
       
            IDictionary<int, IEnumerable<string>>? data = await this.saver.GetAll("user.json");
            if (data is null) return BadRequest("Ошибка");
            bool numb = await this.saver.Remove("user.json", data, number);
            if (!numb) return BadRequest("Такой игры нет");
            return Ok(numb
            );


        }
    }
}