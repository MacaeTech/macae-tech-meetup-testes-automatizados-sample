using System.Data;
using Dapper;
using Npgsql;

namespace Elleva.MacaeTech.ECommerce.IntegrationTests
{
    public class BaseIntegrationTest
    {
        protected string GenerateConnectionString(string database)
        {
            return $"User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database={database};Pooling=false";
        }
        
        protected void PrepararBanco(string databaseTeste)
        {
            var connectionStringMaster = GenerateConnectionString("postgres");

            using (var conexao = new NpgsqlConnection(connectionStringMaster))
            {
                conexao.Open();

                var cmd = conexao.CreateCommand();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"create database {databaseTeste} with owner postgres;";
                cmd.ExecuteNonQuery();
                
                conexao.Close();
            }

            var connectionStringDatabaseTeste = GenerateConnectionString(databaseTeste);

            var scriptEstrutura= @"
            create schema core;

            create table if not exists core.carrinho
            (
                codigo uuid not null
                constraint carrinho_pk
                primary key,
                codigo_cupom varchar(50)
            );

            alter table core.carrinho owner to postgres;

            create unique index if not exists carrinho_codigo_uindex
                on core.carrinho (codigo);

            create table if not exists core.item_carrinho
            (
                codigo uuid not null
                constraint item_carrinho_pk
                primary key,
                codigo_carrinho uuid not null,
                quantidade integer not null,
                valor_unitario numeric not null,
                nome_produto varchar(100) not null
            );

            alter table core.item_carrinho owner to postgres;";
            
            using (var conexao = new NpgsqlConnection(connectionStringDatabaseTeste))
            {
                conexao.Open();
                var cmd = conexao.CreateCommand();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = scriptEstrutura;
                cmd.ExecuteNonQuery();
                
                conexao.Close();
            }
        }

        protected void DroparBanco(string databaseTeste)
        {
            var connectionStringMaster = GenerateConnectionString("postgres");

            using (var conexao = new NpgsqlConnection(connectionStringMaster))
            {
                conexao.Open();

                var cmd = conexao.CreateCommand();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"drop database {databaseTeste};";
                cmd.ExecuteNonQuery();
                
                conexao.Close();
            }
        }
    }
}