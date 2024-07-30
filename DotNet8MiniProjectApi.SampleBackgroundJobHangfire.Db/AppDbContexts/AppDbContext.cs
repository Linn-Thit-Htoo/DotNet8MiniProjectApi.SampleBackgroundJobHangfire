namespace DotNet8MiniProjectApi.SampleBackgroundJobHangfire.Db.AppDbContexts;

public partial class AppDbContext : DbContext
{
    public AppDbContext() { }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public virtual DbSet<TblSetup> TblSetups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblSetup>(entity =>
        {
            entity.HasKey(e => e.SetupId);

            entity.ToTable("Tbl_Setup");

            entity.Property(e => e.SetupId).HasMaxLength(50);
            entity.Property(e => e.SetupCode).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
