using System;
using System.Collections.Generic;

abstract class Llamada
{
    protected string origen;
    protected string destino;
    protected int duracion;

    public Llamada(string origen, string destino, int duracion)
    {
        this.origen = origen;
        this.destino = destino;
        this.duracion = duracion;
    }

    public abstract double CalcularCosto(); 

    public override string ToString()
    {
        return $"Origen: {origen}, Destino: {destino}, Duración: {duracion}s";
    }
}

class LlamadaLocal : Llamada
{
    public LlamadaLocal(string origen, string destino, int duracion)
        : base(origen, destino, duracion) { }

    public override double CalcularCosto()
    {
        return duracion * 0.15; 
    }
}

class LlamadaProvincial : Llamada
{
    private int franja;

    public LlamadaProvincial(string origen, string destino, int duracion, int franja)
        : base(origen, destino, duracion)
    {
        this.franja = franja;
    }

    public override double CalcularCosto()
    {
        double precio = 0;

        switch (franja)
        {
            case 1: precio = 0.20; break;
            case 2: precio = 0.25; break;
            case 3: precio = 0.30; break;
        }

        return duracion * precio;
    }
}

class Centralita
{
    private List<Llamada> llamadas = new List<Llamada>();

    public void RegistrarLlamada(Llamada llamada)
    {
        llamadas.Add(llamada);
    }

    public int TotalLlamadas()
    {
        return llamadas.Count;
    }

    public double TotalFacturacion()
    {
        double total = 0;
        foreach (var llamada in llamadas)
        {
            total += llamada.CalcularCosto();
        }
        return total;
    }

    public void MostrarLlamadas()
    {
        foreach (var llamada in llamadas)
        {
            Console.WriteLine(llamada);
        }
    }
}

class Practica2
{
    static void Main()
    {
        Centralita central = new Centralita();

        central.RegistrarLlamada(new LlamadaLocal("809123", "809456", 60));
        central.RegistrarLlamada(new LlamadaProvincial("809123", "829456", 120, 1));
        central.RegistrarLlamada(new LlamadaProvincial("809789", "849111", 90, 3));

        central.MostrarLlamadas();

        Console.WriteLine("\nTotal llamadas: " + central.TotalLlamadas());
        Console.WriteLine("Facturación total: $" + central.TotalFacturacion());
    }
}