namespace OidtWpf.DataModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Collections.Generic;

    public class DataContext : DbContext
    {
        // Контекст настроен для использования строки подключения "DataContext" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "OidtWpf.DataModel.DataContext" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "DataContext" 
        // в файле конфигурации приложения.
        public DataContext()
            : base("name=DataContext")
        {

            //Events.Include(x => x.Parameters).ToList();

        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Parameters> Parameters { get; set; }
        public DbSet<DayStat> DayStats { get; set; }
        public DbSet<StageStat> StagesStats { get; set; }
        public DbSet<StageStatDay> StagesStatDays { get; set; }
        public DbSet<ItemStatDay> ItemStatDays { get; set; }
        public DbSet<ItemStat> ItemStats { get; set; }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}