namespace OidtWpf.DataModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Collections.Generic;

    public class DataContext : DbContext
    {
        // �������� �������� ��� ������������� ������ ����������� "DataContext" �� ����� ������������  
        // ���������� (App.config ��� Web.config). �� ��������� ��� ������ ����������� ��������� �� ���� ������ 
        // "OidtWpf.DataModel.DataContext" � ���������� LocalDb. 
        // 
        // ���� ��������� ������� ������ ���� ������ ��� ��������� ���� ������, �������� ������ ����������� "DataContext" 
        // � ����� ������������ ����������.
        public DataContext()
            : base("name=DataContext")
        {
            Database.CommandTimeout = 640;
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<DayStat> DayStats { get; set; }
        public DbSet<StageStat> StagesStats { get; set; }
        public DbSet<StageStatDay> StagesStatDays { get; set; }
        public DbSet<ItemStatDay> ItemStatDays { get; set; }
        public DbSet<ItemStat> ItemStats { get; set; }
        public DbSet<Prediction> Prediction { get; set; }
        public DbSet<Users> Users { get; set; }

        // �������� DbSet ��� ������� ���� ��������, ������� ��������� �������� � ������. �������������� �������� 
        // � ��������� � ������������� ������ Code First ��. � ������ http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}