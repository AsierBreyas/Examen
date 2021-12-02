using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static System.Console;


public class InstitutoContext : DbContext
{

    public DbSet<Alumno> Alumnos {get; set;}
    public DbSet<Modulo> Modulos {get; set;}
    public DbSet<Matricula> Matriculas {get; set;}
    public string connString { get; private set; }

    public InstitutoContext()
    {
        var database = "EF05Asier"; // "EF{XX}Nombre" => EF00Santi
        connString = $"Server=185.60.40.210\\SQLEXPRESS,58015;Database={database};User Id=sa;Password=Pa88word;MultipleActiveResultSets=true";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(connString);
    protected override void OnModelCreating(ModelBuilder modelBuilder){
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
    public string Nombre {get; set;}
    public int Edad {get; set;}
    public decimal Efectivo {get; set;}
    public string Pelo {get; set;}
    public List<Matricula> Matriculas {get;} = new List<Matricula>();
    /*

    */
}
public class Modulo
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int ModuloId { get; set; }
    public string Titulo {get; set;}
    public int Creditos {get; set;}
    public int Curso {get; set;}
    public List<Matricula> Matriculas {get;} = new List<Matricula>();
    /*

    */

}
public class Matricula
{
    public int MatriculaId {get; set;}
    public int AlumnoId {get; set;}
    public int ModuloId {get; set;}
    public Alumno alumno {get; set;}
    public Matricula matricula {get; set;}
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
            db.Add(new Alumno{AlumnoId = 1, Nombre = "Eustaquio", Edad = 75, Efectivo = 60.54M, Pelo = "Castaño"});
            db.Add(new Alumno{AlumnoId = 2, Nombre = "Habichuela", Edad = 45, Efectivo = 60.54M, Pelo = "Moreno"});
            db.Add(new Alumno{AlumnoId = 3, Nombre = "El gran mago pajas", Edad = 12, Efectivo = 60.54M, Pelo = "Moreno"});
            db.Add(new Alumno{AlumnoId = 4, Nombre = "Willy", Edad = 2, Efectivo = 60.54M, Pelo = "Castaño"});
            db.Add(new Alumno{AlumnoId = 5, Nombre = "Asdrubal", Edad = 17, Efectivo = 60.54M, Pelo = "Rubio"});
            db.Add(new Alumno{AlumnoId = 6, Nombre = "Astolfo", Edad = 20, Efectivo = 60.54M, Pelo = "Castaño"});
            db.Add(new Alumno{AlumnoId = 7, Nombre = "Mari Carmen", Edad = 34, Efectivo = 60.54M, Pelo = "Rubio"});

            // Añadir Módulos
            // Id de 1 a 10
            db.Add(new Modulo{ModuloId = 1, Titulo = "Titulo1", Creditos = 20, Curso = 1});
            db.Add(new Modulo{ModuloId = 2, Titulo = "Titulo2", Creditos = 20, Curso = 2});
            db.Add(new Modulo{ModuloId = 3, Titulo = "Titulo3", Creditos = 20, Curso = 5});
            db.Add(new Modulo{ModuloId = 4, Titulo = "Titulo4", Creditos = 20, Curso = 6});
            db.Add(new Modulo{ModuloId = 5, Titulo = "Titulo5", Creditos = 20, Curso = 3});
            db.Add(new Modulo{ModuloId = 6, Titulo = "Titulo6", Creditos = 20, Curso = 1});
            db.Add(new Modulo{ModuloId = 7, Titulo = "Titulo7", Creditos = 20, Curso = 6});
            db.Add(new Modulo{ModuloId = 8, Titulo = "Titulo8", Creditos = 20, Curso = 5});
            db.Add(new Modulo{ModuloId = 9, Titulo = "Titulo9", Creditos = 20, Curso = 3});
            db.Add(new Modulo{ModuloId = 10, Titulo = "Titulo10", Creditos = 20, Curso = 2});
            db.SaveChanges();
            // Matricular Alumnos en Módulos
            var alumnos = db.Alumnos;
            var modulos = db.Modulos;
            foreach(Alumno alumno in alumnos){
                foreach(Modulo modulo in modulos){
                    db.Add(new Matricula{AlumnoId = alumno.AlumnoId, ModuloId = modulo.ModuloId});
                }
            }

        }
    }

    static void BorrarMatriculaciones()
    {
        // Borrar las matriculas d
        // AlumnoId multiplo de 3 y ModuloId Multiplo de 2;
        // AlumnoId multiplo de 2 y ModuloId Multiplo de 5;
    }
    static void RealizarQuery()
    {
        using (var db = new InstitutoContext())
        {
            // Las queries que se piden en el examen

        }
    }

    static void Main(string[] args)
    {
        GenerarDatos();
        BorrarMatriculaciones();
        RealizarQuery();
    }

}