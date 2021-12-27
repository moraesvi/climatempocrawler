using CTCrawler.Common;
using CTCrawler.Parse.Interface;
using CTCrawler.Parse.Model;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace CTCrawler.Parse
{
    public class CTFifteenDaysParse : ICTFifteenDaysParse
    {
        private const int WHEATHER_HTML_NODE_ID = 1;
        public IEnumerable<WeatherCT> Parse(string html)
        {
            if (string.IsNullOrEmpty(html))
                yield break;

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            List<HtmlNode> nodeResult = htmlDoc.DocumentNode.SelectNodes("//div[@data-visualization-content]//section")
                                                            ?.ToList();

            if (nodeResult == null)
                yield break;

            for (int indice = 0; indice < nodeResult.Count(); indice++)
            {
                if (indice == 13)
                {

                }
                HtmlNode node = nodeResult[indice];
                (int, string) tpPrevisaoData = node.SelectNodes("./div[@class='date-inside-circle' or @class='date-inside-circlewith-alert']")
                                                   .Select(nd =>
                                                   {
                                                       int dayOfWeek = nd.FirstChild.InnerText.OnlyNumbers().ToInt16();
                                                       string dayOfWeekDesc = nd.FirstChild.NextSibling.InnerText.Trim();
                                                       if (nd.FirstChild.InnerText.OnlyNumbers().ToInt16() == 0)
                                                       {
                                                           dayOfWeek = nd.FirstChild.NextSibling.NextSibling.InnerText.OnlyNumbers().ToInt16();
                                                           dayOfWeekDesc = nd.FirstChild.NextSibling.NextSibling.NextSibling.InnerText.Trim();
                                                       }
                                                       return (dayOfWeek, dayOfWeekDesc);
                                                   })
                                                   .FirstOrDefault();

                HtmlNode nodePeriodosDia = node.SelectSingleNode("//div[contains(@class,'periods-icons')]");

                HtmlNode[] nodes = node.SelectNodes("./div")
                                       .ToArray();

                List<HtmlNode> wheatherNode = nodes[WHEATHER_HTML_NODE_ID].SelectNodes("./div")
                                                                          .ToList();

                WeatherCT weatherModel = wheatherNode.FirstOrDefault()
                                                     .SelectNodes("./div")
                                                     .Select(nd => new WeatherCT(nd.SelectSingleNode("img[1]").Attributes["data-src"].Value.Trim())
                                                     {
                                                         Title = nd.SelectSingleNode("img[1]").Attributes["title"].Value.Trim(),
                                                         Day = tpPrevisaoData.Item1,
                                                         WeekDay = tpPrevisaoData.Item2,
                                                         MinTemp = nd.SelectSingleNode(".//img[contains(@src, 'ic-arrow-min')]//following-sibling::span").InnerText.OnlyNumbers().ToInt16(),
                                                         MaxTemp = nd.SelectSingleNode(".//img[contains(@src, 'ic-arrow-max')]//following-sibling::span").InnerText.OnlyNumbers().ToInt16(),
                                                         Precept = new WeatherPreceptCT
                                                         {
                                                             Raindrop = new WeatherRaindropCT
                                                             {
                                                                 RaindropCount = (short)(nd.SelectNodes(".//img[contains(@data-src, 'gota-azul') or contains(@src, 'gota-azul')]")?.Count ?? 0),
                                                                 WithouthRaindropCount = (short)(nd.SelectNodes(".//img[contains(@data-src, 'gota-cinza') or contains(@src, 'gota-cinza')]")?.Count ?? 0)
                                                             },
                                                             Percent = nd.SelectSingleNode("div[2]").InnerText.OnlyNumbers().ToInt16(),
                                                         },
                                                         Desc = nd.ParentNode.SelectSingleNode("p").InnerText.Trim(),
                                                         Periods = nodePeriodosDia.SelectNodes("./div")
                                                                                  .Select(nd => new WeatherDayDetailCT(nd.SelectSingleNode("img").Attributes["data-src"].Value.Trim())
                                                                                  {
                                                                                      Period = nd.SelectSingleNode("p").InnerText.Trim(),
                                                                                      Desc = nd.SelectSingleNode("img").Attributes["title"].Value.Trim(),
                                                                                  }).ToArray()
                                                     })
                                                     .SingleOrDefault();

                yield return weatherModel;
            }
        }
    }
}
