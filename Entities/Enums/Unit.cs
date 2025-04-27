namespace Crud.Entities.Enums;

public enum Unit 
{
    Gram = 0,
    Kilogram = 1,
    Milliliter = 2,
    Liter = 3,
    Piece = 4,
    Cup = 5,
    Tablespoon = 6,
    Teaspoon = 7,
    Ounce = 8,
    Pound = 9,
    FluidOunce = 10,
}

public static class UnitExtensions
{
    public static List<string> GetAllLabels() => [
        "g",
        "kg",
        "ml",
        "L",
        "pcs",
        "c",
        "tbsp",
        "tsp",
        "oz",
        "lb",
        "fl oz"
    ];

    private static readonly Dictionary<Unit, string> _abbreviations = new() 
    {
        { Unit.Gram, "g" },
        { Unit.Kilogram, "kg" },
        { Unit.Milliliter, "ml" },
        { Unit.Liter, "L" },
        { Unit.Piece, "pcs" },
        { Unit.Cup, "c" },
        { Unit.Tablespoon, "tbsp" },
        { Unit.Teaspoon, "tsp" },
        { Unit.Ounce, "oz" },
        { Unit.Pound, "lb" },
        { Unit.FluidOunce, "fl oz" }
    };

    public static string ToLabel(this Unit unit) => _abbreviations[unit];

    public static Unit? FromLabel(string label) 
    {
        return label switch
        {
            "g"     => Unit.Gram,
            "kg"    => Unit.Kilogram,
            "ml"    => Unit.Milliliter,
            "L"     => Unit.Liter,
            "pcs"   => Unit.Piece,
            "c"     => Unit.Cup,
            "tbsp"  => Unit.Tablespoon,
            "tsp"   => Unit.Teaspoon,
            "oz"    => Unit.Ounce,
            "lb"    => Unit.Pound,
            "fl oz" => Unit.FluidOunce,
            _ => null
        };
    }

    private static readonly HashSet<Unit> massUnits = [Unit.Gram, Unit.Kilogram, Unit.Ounce, Unit.Pound];
    private static readonly HashSet<Unit> volumeUnits = [Unit.Milliliter, Unit.Liter, Unit.Cup, Unit.Tablespoon, Unit.Teaspoon, Unit.FluidOunce];

    private static bool SameType(Unit unit1, Unit unit2) 
    {
        if (unit1 == unit2) return true;
        if (massUnits.Contains(unit1) && massUnits.Contains(unit2)) return true;
        if (volumeUnits.Contains(unit1) && volumeUnits.Contains(unit2)) return true;
        return false;
    }

    private static double? ToBaseUnit(this Unit unit)
    {
        return unit switch
        {
            Unit.Gram => 1.0,
            Unit.Kilogram => 1000.0,
            Unit.Ounce => 28.3495,
            Unit.Pound => 453.592,
            Unit.Milliliter => 1.0,
            Unit.Liter => 1000.0,
            Unit.Cup => 236.588,
            Unit.Tablespoon => 14.7868,
            Unit.Teaspoon => 4.92892,
            Unit.FluidOunce => 29.5735,
            Unit.Piece => 1.0,
            _ => null
        };
    }

    public static double? Convert(Unit fromUnit, Unit toUnit)
    {
        if (!SameType(fromUnit, toUnit)) return null;
        if (fromUnit == toUnit) return 1.0;
        double? fromBase = fromUnit.ToBaseUnit();
        double? toBase = toUnit.ToBaseUnit();
        if (fromBase == null || toBase == null) return null;
        return fromBase / toBase;
    }
}