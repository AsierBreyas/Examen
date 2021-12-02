using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static System.Console;


public class InstitutoContext : DbContext
{

    public string connString { get; private set; }

    public InstitutoContext()
    {
        var database = ""; // "EF{XX}Nombre" => EF00Santi
        connString = $"Server=185.60.40.210\\SQLEXPRESS,58015;Database={database};User Id=sa;Password=Pa88word;MultipleActiveResultSets=true";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(connString);

}
public class Alumno
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int AlumnoId { get; set; }
    /*

    */
}
public class Modulo
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int ModuloId { get; set; }
    /*

    */

}
public class Matricula
{

}

class Program
{
    static void GenerarDatos()
    {
        using (var db = new InstitutoContext())
        {
            // Borrar todo

            // Añadir Alumnos
            // Id de 1 a 7

            // Añadir Módulos
            // Id de 1 a 10

            // Matricular Alumnos en Módulos

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