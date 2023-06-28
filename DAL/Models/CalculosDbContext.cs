using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class CalculosDbContext : DbContext
{
    public CalculosDbContext()
    {
    }

    public CalculosDbContext(DbContextOptions<CalculosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calculo> Calculos { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-C8CJCQK\\SQLEXPRESS;Database=CalculosDB;User Id=sa;Password=sqlserver;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calculo>(entity =>
        {
            entity.ToTable("Calculo");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Usuario).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    public IEnumerable<Calculo> BuscarCalculos(String? name, long max, long min)
    {
        var parameters = new List<SqlParameter>();

        if(name == null)
        {
            parameters.Add(new SqlParameter("@Nombre",DBNull.Value));
        }
        else
        {
            parameters.Add(new SqlParameter("@Nombre", name));
        }
        parameters.Add(new SqlParameter("@RespuestaMax", max));
        parameters.Add(new SqlParameter("@RespuestaMin", min));

        var calculos = Calculos.FromSqlRaw("EXEC BuscarCalculos @Nombre, @RespuestaMax, @RespuestaMin",parameters.ToArray()).ToList();
        return calculos;
    }

    public void InsertarCalculo(string name, long value)
    {
        var nombreParam = new SqlParameter("@Nombre", name);
        var calculoParam = new SqlParameter("@Calculo", value);
        Database.ExecuteSqlRaw("EXEC InsertarCalculo @Nombre, @Calculo", nombreParam, calculoParam);
        SaveChanges();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
