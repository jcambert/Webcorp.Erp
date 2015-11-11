using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Model
{
    /*public sealed partial class MaterialProvider
    {
        private readonly Dictionary<string, Material> materials = new Dictionary<string, Material>();

        static MaterialProvider _instance;
        static MaterialProvider()
        {
            _instance = new MaterialProvider();
        }

        public static MaterialProvider Default => _instance;

        private MaterialProvider()
        {
            initialize();
        }
        partial void initialize();

        public void Register(Material material)
        {
            if (material == null) throw new ArgumentNullException("material cannot be null");
            if (materials.ContainsKey(material.Number)) throw new ArgumentException("material :" + material.Number + " is already registered");
            materials[material.Number] = material;
        }

        public List<Material> Materials => materials.Values.ToList();


        public Material this[string number] =>materials[number];
    }*/
}
