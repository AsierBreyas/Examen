using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static System.Console;


public class InstitutoContext : DbContext
{

    public DbSet<Alumno> Alumnos { get; set; }
    public DbSet<Modulo> Modulos { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }
    public string connString { get; private set; }

    public InstitutoContext()
    {
        var database = "EF05Asier"; // "EF{XX}Nombre" => EF00Santi
        connString = $"Server=185.60.40.210\\SQLEXPRESS,58015;Database={database};User Id=sa;Password=Pa88word;MultipleActiveResultSets=true";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(connString);
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Matricula>().HasIndex(m => new
        {
            m.AlumnoId,
            m.ModuloId
        }).IsUnique();

    }

}
public class Alumno
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int AlumnoId { get; set; }
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public decimal Efectivo { get; set; }
    public string Pelo { get; set; }
    public List<Matricula> Matriculas { get; } = new List<Matricula>();
    public override string ToString() => $"{AlumnoId}: {Nombre}: {Edad}: {Efectivo}: {Pelo} ";
    /*

    */
}
public class Modulo
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int ModuloId { get; set; }
    public string Titulo { get; set; }
    public int Creditos { get; set; }
    public int Curso { get; set; }
    public List<Matricula> Matriculas { get; } = new List<Matricula>();
    public override string ToString() => $"{ModuloId}: {Titulo}: {Creditos}: {Curso} ";
    /*

    */

}
public class Matricula
{
    public Alumno Alumno { get; set; }
    public Modulo Modulo { get; set; }
    public int MatriculaId { get; set; }

    public int AlumnoId { get; set; }

    public int ModuloId { get; set; }
    public override string ToString() => $"{MatriculaId}: {AlumnoId}: {ModuloId} ";
}

class Program
{
    static void GenerarDatos()
    {
        using (var db = new InstitutoContext())
        {
            // Borrar todo
            db.Alumnos.RemoveRange(db.Alumnos);
            db.Modulos.RemoveRange(db.Modulos);
            db.Matriculas.RemoveRange(db.Matriculas);

            // Añadir Alumnos
            // Id de 1 a 7
            db.Add(new Alumno { AlumnoId = 1, Nombre = "Eustaquio", Edad = 75, Efectivo = 60.54M, Pelo = "Castaño" });
            db.Add(new Alumno { AlumnoId = 2, Nombre = "Habichuela", Edad = 45, Efectivo = 60.54M, Pelo = "Moreno" });
            db.Add(new Alumno { AlumnoId = 3, Nombre = "El gran mago pajas", Edad = 12, Efectivo = 60.54M, Pelo = "Moreno" });
            db.Add(new Alumno { AlumnoId = 4, Nombre = "Willy", Edad = 2, Efectivo = 60.54M, Pelo = "Castaño" });
            db.Add(new Alumno { AlumnoId = 5, Nombre = "Asdrubal", Edad = 17, Efectivo = 60.54M, Pelo = "Rubio" });
            db.Add(new Alumno { AlumnoId = 6, Nombre = "Astolfo", Edad = 20, Efectivo = 60.54M, Pelo = "Castaño" });
            db.Add(new Alumno { AlumnoId = 7, Nombre = "Mari Carmen", Edad = 34, Efectivo = 60.54M, Pelo = "Rubio" });

            // Añadir Módulos
            // Id de 1 a 10
            db.Add(new Modulo { ModuloId = 1, Titulo = "Titulo1", Creditos = 20, Curso = 1 });
            db.Add(new Modulo { ModuloId = 2, Titulo = "Titulo2", Creditos = 20, Curso = 2 });
            db.Add(new Modulo { ModuloId = 3, Titulo = "Titulo3", Creditos = 20, Curso = 5 });
            db.Add(new Modulo { ModuloId = 4, Titulo = "Titulo4", Creditos = 20, Curso = 6 });
            db.Add(new Modulo { ModuloId = 5, Titulo = "Titulo5", Creditos = 20, Curso = 3 });
            db.Add(new Modulo { ModuloId = 6, Titulo = "Titulo6", Creditos = 20, Curso = 1 });
            db.Add(new Modulo { ModuloId = 7, Titulo = "Titulo7", Creditos = 20, Curso = 6 });
            db.Add(new Modulo { ModuloId = 8, Titulo = "Titulo8", Creditos = 20, Curso = 5 });
            db.Add(new Modulo { ModuloId = 9, Titulo = "Titulo9", Creditos = 20, Curso = 3 });
            db.Add(new Modulo { ModuloId = 10, Titulo = "Titulo10", Creditos = 20, Curso = 2 });
            db.SaveChanges();
            // Matricular Alumnos en Módulos
            foreach (Alumno alumno in db.Alumnos.ToList())
            {
                foreach (Modulo modulo in db.Modulos)
                {
                    db.Add(new Matricula { Alumno = alumno, Modulo = modulo });
                }
            }
            db.SaveChanges();

        }
    }

    static void BorrarMatriculaciones()
    {
        // Borrar las matriculas d

        using (var db = new InstitutoContext())
        {
            // AlumnoId multiplo de 3 y ModuloId Multiplo de 2;
            var alumnosM3 = db.Alumnos.Where(x => x.AlumnoId % 3 == 0);
            var modulosM2 = db.Modulos.Where(x => x.ModuloId % 2 == 0);
            foreach (Alumno alumno in alumnosM3)
            {
                foreach (Modulo modulo in modulosM2)
                {
                    var selecMatriculas = db.Matriculas.Where(x => x.AlumnoId == alumno.AlumnoId && x.ModuloId == modulo.ModuloId);
                    db.Matriculas.RemoveRange(selecMatriculas);
                }
            }
            db.SaveChanges();
            // AlumnoId multiplo de 2 y ModuloId Multiplo de 5;
            var alumnosM2 = db.Alumnos.Where(x => x.AlumnoId % 2 == 0);
            var modulosM5 = db.Modulos.Where(x => x.ModuloId % 5 == 0);
            foreach (Alumno alumno in alumnosM2)
            {
                foreach (Modulo modulo in modulosM5)
                {
                    var selecMatriculas = db.Matriculas.SingleOrDefault(x => x.AlumnoId == alumno.AlumnoId && x.ModuloId == modulo.ModuloId);
                    if (selecMatriculas != null)
                    {
                        db.Matriculas.Remove(selecMatriculas);
                    }
                }
            }
            db.SaveChanges();
        }

    }
    static void RealizarQuery()
    {
        using (var db = new InstitutoContext())
        {
            // Las queries que se piden en el examen
            //Filtering
            var filter = db.Alumnos.Where(o => o.AlumnoId == 1);
            var filter2 = db.Modulos.Where(o => o.ModuloId == 1);
            var filter3 = db.Matriculas.Where(o => o.MatriculaId == 682);
            //Anonymus
            var anon1 = db.Alumnos.Select(o => new
            {
                AlumnoId = o.AlumnoId,
                Nombre = o.Nombre
            });
            var anon2 = db.Modulos.Select(o => new
            {
                ModuloId = o.ModuloId,
                Titulo = o.Titulo
            });
            var anon3 = db.Matriculas.Select(o => new
            {
                MatriculaId = o.MatriculaId,
                AlumnoId = o.AlumnoId,
                ModuloId = o.ModuloId
            });
            //Ordering
            var order1 = db.Alumnos.OrderBy(o => o.Nombre);
            var order2 = db.Modulos.OrderBy(o => o.Titulo);
            var order3 = db.Matriculas.OrderBy(o => o.MatriculaId);
            //Joining
            var join = from c in db.Alumnos
                       join o in db.Matriculas on
                       c.AlumnoId equals o.AlumnoId
                       select new
                       {
                           c.AlumnoId,
                           c.Nombre,
                           c.Edad,
                           o.ModuloId
                       };
            //Grouping
            var grouping = from o in db.Matriculas
                           group o by o.AlumnoId into g
                           select new
                           {
                               AlumnoId = g.Key,
                               ModulosToales = g.Count()
                           };
            //Paging
            var paging1 = (from o in db.Alumnos
                           where o.AlumnoId == 1
                           select o).Take(3);
            var paging2 = (from o in db.Modulos
                           where o.ModuloId == 1
                           orderby o.Titulo
                           select o).Skip(2).Take(2);
            var paging3 = (from o in db.Matriculas
                           where o.AlumnoId == 683
                           select o).Take(3);
            //Element Operatos
            var single = (from c in db.Alumnos
                          where c.AlumnoId == 1
                          select c).Single();
            var sDefault = (from c in db.Modulos
                            where c.ModuloId == 1
                            select c).SingleOrDefault();
            var emDefault = (from c in db.Alumnos
                             where c.AlumnoId == 10
                             select c).DefaultIfEmpty(
             new Alumno()).Single();
            var firts = (from o in db.Matriculas
                         where o.MatriculaId == 683
                         orderby o.MatriculaId
                         select o).First();
            var last = (from o in db.Matriculas
                         where o.MatriculaId == 683
                         orderby o.MatriculaId
                         select o).Last();
            var elementAt = (from o in db.Modulos
                         where o.ModuloId == 1
                         orderby o.ModuloId
                         select o).ElementAt(4);

        }
    }

    static void Main(string[] args)
    {
        GenerarDatos();
        BorrarMatriculaciones();
        RealizarQuery();
    }

}