using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Xml.Linq;
using Bankamatik.Core.Entities;

namespace Bankamatik.Business.Services
{
    public class KurService
    {
        public List<Kur> KurlariGetir()
        {
            var kurlar = new List<Kur>();
            try
            {
                string url = "https://www.tcmb.gov.tr/kurlar/today.xml";

                using (var client = new WebClient())
                {
                    string xmlContent = client.DownloadString(url);
                    var xDoc = XDocument.Parse(xmlContent);

                    var tarihAttr = xDoc.Root?.Attribute("Tarih")?.Value;
                    DateTime.TryParseExact(tarihAttr, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tarih);

                    foreach (var item in xDoc.Descendants("Currency"))
                    {
                        var kur = new Kur
                        {
                            Kod = item.Attribute("Kod")?.Value,
                            Isim = item.Element("Isim")?.Value,
                            Alis = decimal.TryParse(item.Element("ForexBuying")?.Value.Replace('.', ','), out var alis) ? alis : 0,
                            Satis = decimal.TryParse(item.Element("ForexSelling")?.Value.Replace('.', ','), out var satis) ? satis : 0,
                            Tarih = tarih
                        };
                        if (kur.Kod == "JPY")
                        {
                            kur.Alis /= 100;
                            kur.Satis /= 100;
                        }

                        kurlar.Add(kur);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return kurlar;
        }
    }
}
