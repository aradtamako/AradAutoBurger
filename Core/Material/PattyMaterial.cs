using System.Collections.Generic;
using System.Drawing;

namespace Core.Material
{
    /// <summary>
    /// パティ
    /// </summary>
    public class PattyMaterial : IMaterial
    {
        public Point TeacherCordinate { get; } = new Point(71, 169);
        public List<Color> Material { get; set; } = new List<Color>();
    }
}
