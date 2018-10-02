namespace OrchardProject.Models
{
    public class Chemical
    {
        public int ChemicalID { get; set; }
        public string Name { get; set; }
        public int UnitsInBox { get; set; }
        public double QuantityInUnit { get; set; }
        public string QuantityType { get; set; }
        public string Description { get; set; }

        public Chemical(string name, int QuantityInUnit, string Description, int ChemicalID, int UnitsInBox,
            string QuantityType)
        {
            this.Name = name;
            this.UnitsInBox = UnitsInBox;
            this.QuantityInUnit = QuantityInUnit;
            this.Description = Description;
            this.ChemicalID = ChemicalID;
            this.QuantityType = QuantityType;
        }

        public Chemical()
        {
            this.Name = null;
            this.UnitsInBox = -1;
            this.QuantityInUnit = -1;
            this.Description = null;
            this.ChemicalID = -1;
            this.QuantityType = null;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}