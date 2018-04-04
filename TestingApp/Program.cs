using System;
using Castle.Windsor;
using Witcherius.BL.Config;
using Witcherius.BL.DataTransferObjects.Monster;
using Witcherius.BL.Facades;

namespace TestingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Install(new BlInstaller());

            var service = container.Resolve<MonsterFacade>();
            var user = new MonsterDto() { Name = "lol", Experience = 22};
            var monsterId = service.CreateMonster(user);
            Console.WriteLine(monsterId);
            Console.ReadKey();
        }
    }
}
