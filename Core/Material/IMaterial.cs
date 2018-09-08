using System.Collections.Generic;
using System.Drawing;

namespace Core.Material
{
    public interface IMaterial
    {
        /// <summary>
        /// 教師データの始点
        /// </summary>
        Point TeacherCordinate { get; }

        /// <summary>
        /// 具材のマスタデータ
        /// </summary>
        List<Color> Material { get; set; }
    }
}
