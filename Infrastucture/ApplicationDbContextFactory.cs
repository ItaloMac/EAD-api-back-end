using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastucture
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext> //Criar instancias ApplicationDbContext em tempo de design
    {
        public ApplicationDbContext CreateDbContext(string[] args) //metodo para criar e devolver uma instacnia
        {
            var configuration = new ConfigurationBuilder() //constroi uma configuração a partir de varias fontes
                .SetBasePath(Directory.GetCurrentDirectory()) //define o diretorio base para busca de arquivo de configuração
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) //define appsettings como o atquivo de configuracao, obrigatorio, carregado se for alterado
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>(); //configura as opcoes de aplication db context
            var connectionString = configuration.GetConnectionString("DefaultConnection"); //obtem a string de conexao

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)); //configura para usar mysql como banco de dados, detectar automaaticamente a versão

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
