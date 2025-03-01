﻿namespace ShapeFactory;

using Bogus;
using Domain;

public class Painter
{
    private readonly IEnumerable<string> _colors;
    private readonly Faker _faker;

    public Painter(Faker faker)
    {
        _faker = faker;
        _colors = new[] { "R", "G", "B" };
    }

    public IEnumerable<Shape> Paint(IEnumerable<Shape> shapes)
    {
        foreach (var shape in shapes)
        {
            shape.Color = _faker.PickRandom(_colors).OrNull(_faker, .2f);
            Console.WriteLine($"Paint({shape}) => {shape}");
            yield return shape;
        }
    }

    public IEnumerable<Shape> OtherPaint(IEnumerable<Shape> shapes)
    {
        var random = new Random();

        var enumerator = shapes.GetEnumerator();
        while (enumerator.MoveNext())
            switch (random.Next(0, 4))
            {
                case 1:
                    enumerator.Current.Color = "R";
                    yield return enumerator.Current;
                    break;
                case 2:
                    enumerator.Current.Color = "G";
                    yield return enumerator.Current;
                    break;
                case 3:
                    enumerator.Current.Color = "B";
                    yield return enumerator.Current;
                    break;
                default:
                    // enumerator.Current.Color = null;
                    yield return enumerator.Current;
                    break;
            }
    }
}