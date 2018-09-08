using Core.Material;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class AutoBurger
    {
        private static PattyMaterial Patty { get; } = new PattyMaterial();
        private static CheeseMaterial Cheese { get; } = new CheeseMaterial();
        private static LettuceMaterial Lettuce { get; } = new LettuceMaterial();
        private static TomatoMaterial Tomato { get; } = new TomatoMaterial();
        private IMaterial[] Materials { get; } = new IMaterial[] { Patty, Cheese, Lettuce, Tomato };

        public AutoBurger(bool emitStudyResult = false)
        {
            DoStudy(emitStudyResult);
        }

        /// <summary>
        /// 教師データから具材マスタを作成する
        /// </summary>
        private void DoStudy(bool emitStudyResult = false)
        {
            using (var img = new Bitmap("imgs/teacher.png"))
            {
                const int length = 30;

                foreach (var material in Materials)
                {
                    for (var i = 0; i < length; i++)
                    {
                        var color = img.GetPixel(material.TeacherCordinate.X + i, material.TeacherCordinate.Y);
                        material.Material.Add(color);

                        if (emitStudyResult)
                        {
                            img.SetPixel(material.TeacherCordinate.X + i, material.TeacherCordinate.Y, Color.Blue);
                        }
                    }
                }

                if (emitStudyResult)
                {
                    img.Save("study_result.png", ImageFormat.Png);
                }
            }
        }

        /// <summary>
        /// 具材を判定する
        /// </summary>
        public bool CheckMaterial<T>(Bitmap img, T material, int x, int y, int offset) where T : IMaterial
        {
            if (x + material.Material.Count < img.Width && offset < material.Material.Count)
            {
                if (img.GetPixel(x + offset, y) == material.Material[offset])
                {
                    return (offset == material.Material.Count - 1) ?
                        true : CheckMaterial(img, material, x, y, ++offset);
                }
            }

            return false;
        }

        public IEnumerable<IMaterial> GetMaterials(string fileName)
        {
            using (var img = new Bitmap(fileName))
            {
                foreach (var material in GetMaterials(img))
                {
                    yield return material;
                }
            }
        }

        public IEnumerable<IMaterial> GetMaterials(Bitmap img)
        {
            for (var y = img.Height - 1; y >= 0; y--)
            {
                for (var x = 0; x < img.Width; x++)
                {
                    foreach (var material in Materials)
                    {
                        var result = CheckMaterial(img, material, x, y, 0);
                        if (result)
                        {
                            yield return material;
                        }
                    }
                }
            }
        }
    }
}
