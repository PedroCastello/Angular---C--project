using Microsoft.AspNetCore.Mvc;
using PrimeiraApi.Models;

namespace PrimeiraApi.Rotas;

public static class PessoaRotas
{

    public static List<Pessoa> Pessoas = new()
    {
        new (Guid.NewGuid(), "Neymar"),
        new (Guid.NewGuid(), "Messi"),
        new (Guid.NewGuid(), "Ronaldo")
    };
    public static void MapPessoaRotas(this WebApplication app)
    {
        app.MapGet("/pessoas", () => Pessoas);
 
        app.MapGet("/pessoas/{nome}", (string nome) => Pessoas.Find(x => x.Nome == nome));

        app.MapPost("/pessoas", ( HttpContext request, Pessoa pessoa) =>
        {
          //  if (pessoa.Nome == "Rodolfo")
              //  return Results.BadRequest(new {message = "Error: Nome Rodolfo banido"});

              var nome = request.Request.Query["name"];
              
            Pessoas.Add(pessoa);
            return Results.Ok(pessoa);
        });

        app.MapPut("/pessoas/{id:guid}", (Guid id,Pessoa pessoa) =>
        {
            var encontrado = Pessoas.Find(x => x.Id == id);

            if (encontrado == null)
                return Results.NotFound();

            encontrado.Nome = pessoa.Nome;

            return Results.Ok(encontrado);
        });


    }
}