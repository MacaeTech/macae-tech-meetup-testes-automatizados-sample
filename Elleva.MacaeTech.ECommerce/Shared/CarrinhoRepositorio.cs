using System;
using System.Linq;
using Dapper;
using Npgsql;

namespace Elleva.MacaeTech.ECommerce.Shared
{   
    public class CarrinhoRepositorio : ICarrinhoRepositorio
    {
        
        private readonly string _connectionString;

        public CarrinhoRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public void Salvar(Carrinho carrinho)
        {
            const string CarrinhoInsertSql = @"
            INSERT INTO core.carrinho (codigo, codigo_cupom) 
            VALUES (@Codigo, @CodigoCupom)";
            
            const string ItemCarrinhoInsertSql = @"
            INSERT INTO core.item_carrinho (codigo, codigo_carrinho, quantidade, valor_unitario, nome_produto) 
            VALUES (@Codigo, @CodigoCarrinho, @Quantidade, @ValorUnitario, @NomeProduto)";
            
            using (var conexao = new NpgsqlConnection(_connectionString))
            {
                conexao.Execute(CarrinhoInsertSql, 
                    new
                {
                    Codigo = carrinho.Codigo,
                    CodigoCupom = carrinho.CodigoCupom
                });

                foreach (var item in carrinho.Itens)
                {
                    conexao.Execute(ItemCarrinhoInsertSql,
                        new
                    {
                        Codigo = item.Codigo,
                        CodigoCarrinho = carrinho.Codigo,
                        Quantidade = item.Quantidade,
                        ValorUnitario = item.ValorUnitario ,
                        NomeProduto = item.NomeProduto
                    });
                }
            }
        }

        public Carrinho Obter(Guid codigo)
        {
            
            const string CarrinhoSelectSql =@"
            SELECT codigo AS Codigo
                 , codigo_cupom AS CodigoCupom 
              FROM core.carrinho 
             WHERE Codigo = @Codigo;
             
            SELECT codigo AS Codigo
                 , quantidade AS Quantidade
                 , valor_unitario AS ValorUnitario
                 , nome_produto AS NomeProduto
              FROM core.item_carrinho 
             WHERE codigo_carrinho = @Codigo;";

            using (var conexao = new NpgsqlConnection(_connectionString))
            {
                var query = conexao.QueryMultiple(CarrinhoSelectSql, new
                {
                    Codigo = codigo
                });

                var carrinho = query.Read<Carrinho>().First();
                carrinho.Itens = query.Read<ItemCarrinho>().ToList();

                return carrinho;
            }
        }
    }

    public interface ICarrinhoRepositorio
    {
        void Salvar(Carrinho carrinho);
        Carrinho Obter(Guid codigo);
    }
}