// ReSharper disable UnusedMember.Global

#pragma warning disable RCS1154

namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     国家代码表
/// </summary>
/// <remarks>
///     ISO3166-1
/// </remarks>
[Export]
public enum CountryCodes
{
    /// <summary>
    ///     多米尼加
    /// </summary>
    [CountryInfo(CallingCode = 1,                     CallingSubCode = ["809", "829", "849"], Alpha3 = "DOM", ShortName = "Dominican Republic"
               , LongName = "The Dominican Republic", CurrencyCode = "DOP",                   Languages = ["es"]
               , UnofficialNames = [
                     "Dominican Republic", "Dominikanische Republik", "République Dominicaine", "República Dominicana", "ドミニカ共和国"
                   , "Dominicaanse Republiek", "多米尼加"
                 ], Timezones = ["America/Santo_Domingo,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.多米尼加))]
    DO = 214

   ,

    /// <summary>
    ///     波多黎各
    /// </summary>
    [CountryInfo(CallingCode = 1,                                     CallingSubCode = ["787", "939"], Alpha3 = "PRI", ShortName = "Puerto Rico"
               , LongName = "The Commonwealth of Puerto Rico",        CurrencyCode = "USD",            Languages = ["es", "en"]
               , UnofficialNames = ["Puerto Rico", "プエルトリコ", "波多黎各"], Timezones = ["America/Puerto_Rico,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.波多黎各))]
    PR = 630

   ,

    /// <summary>
    ///     牙买加
    /// </summary>
    [CountryInfo(CallingCode = 1, CallingSubCode = ["658", "876"], Alpha3 = "JAM", ShortName = "Jamaica", LongName = "Jamaica", CurrencyCode = "JMD"
               , Languages = ["en"], UnofficialNames = ["Jamaica", "Jamaika", "Jamaïque", "ジャマイカ", "牙买加"], Timezones = ["America/Jamaica,-0500"])]
    [ResourceDescription<Ln>(nameof(Ln.牙买加))]
    JM = 388

   ,

    /// <summary>
    ///     圣基茨和尼维斯
    /// </summary>
    [CountryInfo(CallingCode = 1, CallingSubCode = ["869"], Alpha3 = "KNA", ShortName = "Saint Kitts and Nevis", LongName = "Saint Kitts and Nevis"
               , CurrencyCode = "XCD", Languages = ["en"]
               , UnofficialNames = [
                     "Saint Kitts and Nevis", "Föderation St. Kitts und Nevis", "Saint Kitts et Nevis", "Saint Kitts y Nevis", "セントクリストファー・ネイビス"
                   , "Saint Kitts en Nevis", "St. Kitts and Nevis", "St Kitts and Nevis", "圣基茨和尼维斯"
                 ], Timezones = ["America/St_Kitts,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.圣基茨和尼维斯))]
    KN = 659

   ,

    /// <summary>
    ///     特立尼达和多巴哥
    /// </summary>
    [CountryInfo(CallingCode = 1,                                  CallingSubCode = ["868"], Alpha3 = "TTO", ShortName = "Trinidad and Tobago"
               , LongName = "The Republic of Trinidad and Tobago", CurrencyCode = "TTD",     Languages = ["en"]
               , UnofficialNames = [
                     "Trinidad and Tobago", "Trinidad und Tobago", "Trinité et Tobago", "Trinidad y Tobago", "トリニダード・トバゴ", "Trinidad en Tobago"
                   , "特立尼达和多巴哥"
                 ], Timezones = ["America/Port_of_Spain,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.特立尼达和多巴哥))]
    TT = 780

   ,

    /// <summary>
    ///     圣文森特和格林纳丁斯
    /// </summary>
    [CountryInfo(CallingCode = 1, CallingSubCode = ["784"], Alpha3 = "VCT", ShortName = "Saint Vincent and the Grenadines"
               , LongName = "Saint Vincent and the Grenadines", CurrencyCode = "XCD", Languages = ["en"]
               , UnofficialNames = [
                     "Saint Vincent and the Grenadines", "Saint Vincent und die Grenadinen", "Saint-Vincent et les Grenadines"
                   , "San Vicente y Granadinas", "セントビンセントおよびグレナディーン諸島", "Saint Vincent en de Grenadines", "St. Vincent Grenadines"
                   , "St Vincent Grenadines", "圣文森特和格林纳丁斯"
                 ], Timezones = ["America/St_Vincent,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.圣文森特和格林纳丁斯))]
    VC = 670

   ,

    /// <summary>
    ///     多米尼克
    /// </summary>
    [CountryInfo(CallingCode = 1,      CallingSubCode = ["767"], Alpha3 = "DMA", ShortName = "Dominica", LongName = "The Commonwealth of Dominica"
               , CurrencyCode = "XCD", Languages = ["en"], UnofficialNames = ["Dominica", "ドミニカ国", "多米尼克"], Timezones = ["America/Dominica,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.多米尼克))]
    DM = 212

   ,

    /// <summary>
    ///     圣卢西亚
    /// </summary>
    [CountryInfo(CallingCode = 1, CallingSubCode = ["758"], Alpha3 = "LCA", ShortName = "Saint Lucia", LongName = "Saint Lucia", CurrencyCode = "XCD"
               , Languages = ["en"], UnofficialNames = ["Saint Lucia", "Saint-Lucie", "Santa Lucía", "セントルシア", "St. Lucia", "St Lucia", "圣卢西亚"]
               , Timezones = ["America/St_Lucia,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.圣卢西亚))]
    LC = 662

   ,

    /// <summary>
    ///     荷属圣马丁
    /// </summary>
    [CountryInfo(CallingCode = 1,      CallingSubCode = ["721"], Alpha3 = "SXM", ShortName = "Sint Maarten (Dutch part)", LongName = "Sint Maarten"
               , CurrencyCode = "ANG", Languages = ["nl", "en"], UnofficialNames = ["Sint Maarten", "セント・マーチン島", "荷属圣马丁"]
               , Timezones = ["America/Lower_Princes,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.荷属圣马丁))]
    SX = 534

   ,

    /// <summary>
    ///     美属萨摩亚
    /// </summary>
    [CountryInfo(CallingCode = 1, CallingSubCode = ["684"], Alpha3 = "ASM", ShortName = "American Samoa", LongName = "The Territory of American Samoa"
               , CurrencyCode = "USD", Languages = ["en", "sm"]
               , UnofficialNames = [
                     "American Samoa", "Amerikanisch-Samoa", "Samoa américaines", "Samoa Americana", "アメリカ領サモア", "Amerikaans Samoa", "美属萨摩亚"
                 ], Timezones = ["Pacific/Pago_Pago,-1100"])]
    [ResourceDescription<Ln>(nameof(Ln.美属萨摩亚))]
    AS = 016

   ,

    /// <summary>
    ///     关岛
    /// </summary>
    [CountryInfo(CallingCode = 1,      CallingSubCode = ["671"],       Alpha3 = "GUM", ShortName = "Guam", LongName = "The Territory of Guam"
               , CurrencyCode = "USD", Languages = ["en", "ch", "es"], UnofficialNames = ["Guam", "グアム", "关岛"], Timezones = ["Pacific/Guam,+1000"])]
    [ResourceDescription<Ln>(nameof(Ln.关岛))]
    GU = 316

   ,

    /// <summary>
    ///     北马里亚纳群岛
    /// </summary>
    [CountryInfo(CallingCode = 1, CallingSubCode = ["670"], Alpha3 = "MNP", ShortName = "Northern Mariana Islands"
               , LongName = "The Commonwealth of the Northern Mariana Islands", CurrencyCode = "USD", Languages = ["en", "ch"]
               , UnofficialNames = [
                     "Northern Mariana Islands", "Nördliche Marianen", "Mariannes du Nord", "Islas Marianas del Norte", "北マリアナ諸島"
                   , "Noordelijke Marianeneilanden", "北马里亚纳群岛"
                 ], Timezones = ["Pacific/Saipan,+1000"])]
    [ResourceDescription<Ln>(nameof(Ln.北马里亚纳群岛))]
    MP = 580

   ,

    /// <summary>
    ///     蒙特塞拉特
    /// </summary>
    [CountryInfo(CallingCode = 1,    CallingSubCode = ["664"], Alpha3 = "MSR", ShortName = "Montserrat", LongName = "Montserrat", CurrencyCode = "XCD"
               , Languages = ["en"], UnofficialNames = ["Montserrat", "モントセラト", "蒙特塞拉特"], Timezones = ["America/Montserrat,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.蒙特塞拉特))]
    MS = 500

   ,

    /// <summary>
    ///     特克斯和凯科斯群岛
    /// </summary>
    [CountryInfo(CallingCode = 1,                           CallingSubCode = ["649"], Alpha3 = "TCA", ShortName = "Turks and Caicos Islands"
               , LongName = "The Turks and Caicos Islands", CurrencyCode = "USD",     Languages = ["en"]
               , UnofficialNames = [
                     "Turks and Caicos Islands", "Turks- und Caicosinseln", "Îles Turks et Caïcos", "Islas Turks y Caicos", "タークス・カイコス諸島"
                   , "Turks- en Caicoseilanden", "Turks and Caicos", "特克斯和凯科斯群岛"
                 ], Timezones = ["America/Grand_Turk,-0500"])]
    [ResourceDescription<Ln>(nameof(Ln.特克斯和凯科斯群岛))]
    TC = 796

   ,

    /// <summary>
    ///     格林纳达
    /// </summary>
    [CountryInfo(CallingCode = 1,    CallingSubCode = ["473"], Alpha3 = "GRD", ShortName = "Grenada", LongName = "Grenada", CurrencyCode = "XCD"
               , Languages = ["en"], UnofficialNames = ["Grenada", "グレナダ", "格林纳达"], Timezones = ["America/Grenada,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.格林纳达))]
    GD = 308

   ,

    /// <summary>
    ///     百慕大
    /// </summary>
    [CountryInfo(CallingCode = 1,    CallingSubCode = ["441"], Alpha3 = "BMU", ShortName = "Bermuda", LongName = "Bermuda", CurrencyCode = "BMD"
               , Languages = ["en"], UnofficialNames = ["Bermuda", "Bermudes", "Bermudas", "バミューダ", "百慕大"], Timezones = ["Atlantic/Bermuda,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.百慕大))]
    BM = 060

   ,

    /// <summary>
    ///     开曼群岛
    /// </summary>
    [CountryInfo(CallingCode = 1,      CallingSubCode = ["345"], Alpha3 = "CYM", ShortName = "Cayman Islands", LongName = "The Cayman Islands"
               , CurrencyCode = "KYD", Languages = ["en"]
               , UnofficialNames = ["Cayman Islands", "Kaimaninseln", "Îles Caïmans", "Islas Caimán", "ケイマン諸島", "Caymaneilanden", "开曼群岛"]
               , Timezones = ["America/Cayman,-0500"])]
    [ResourceDescription<Ln>(nameof(Ln.开曼群岛))]
    KY = 136

   ,

    /// <summary>
    ///     美属维尔京群岛
    /// </summary>
    [CountryInfo(CallingCode = 1,                                      CallingSubCode = ["340"], Alpha3 = "VIR", ShortName = "Virgin Islands (U.S.)"
               , LongName = "The Virgin Islands of the United States", CurrencyCode = "USD",     Languages = ["en"]
               , UnofficialNames = [
                     "Virgin Islands of the United States", "Amerikanische Jungferninseln", "Îles Vierges américaines"
                   , "Islas Vírgenes de los Estados Unidos", "アメリカ領ヴァージン諸島", "Amerikaanse Maagdeneilanden", "Virgin Islands (U.S.)"
                   , "United States Virgin Islands", "U.S. Virgin Islands", "美属维尔京群岛"
                 ], Timezones = ["America/St_Thomas,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.美属维尔京群岛))]
    VI = 850

   ,

    /// <summary>
    ///     英属维尔京群岛
    /// </summary>
    [CountryInfo(CallingCode = 1, CallingSubCode = ["284"], Alpha3 = "VGB", ShortName = "Virgin Islands (British)", LongName = "The Virgin Islands"
               , CurrencyCode = "USD", Languages = ["en"]
               , UnofficialNames = [
                     "British Virgin Islands", "Britische Jungferninseln", "Îles Vierges britanniques", "Islas Vírgenes del Reino Unido"
                   , "イギリス領ヴァージン諸島", "Britse Maagdeneilanden", "Virgin Islands (British)", "英属维尔京群岛"
                 ], Timezones = ["America/Tortola,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.英属维尔京群岛))]
    VG = 092

   ,

    /// <summary>
    ///     安提瓜和巴布达
    /// </summary>
    [CountryInfo(CallingCode = 1,      CallingSubCode = ["268"], Alpha3 = "ATG", ShortName = "Antigua and Barbuda", LongName = "Antigua and Barbuda"
               , CurrencyCode = "XCD", Languages = ["en"]
               , UnofficialNames = [
                     "Antigua and Barbuda", "Antigua und Barbuda", "Antigua et Barbuda", "Antigua y Barbuda", "アンティグア・バーブーダ", "Antigua en Barbuda"
                   , "安提瓜和巴布达"
                 ], Timezones = ["America/Antigua,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.安提瓜和巴布达))]
    AG = 028

   ,

    /// <summary>
    ///     安圭拉
    /// </summary>
    [CountryInfo(CallingCode = 1,    CallingSubCode = ["264"], Alpha3 = "AIA", ShortName = "Anguilla", LongName = "Anguilla", CurrencyCode = "XCD"
               , Languages = ["en"], UnofficialNames = ["Anguilla", "アンギラ", "安圭拉"], Timezones = ["America/Anguilla,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.安圭拉))]
    AI = 660

   ,

    /// <summary>
    ///     巴巴多斯
    /// </summary>
    [CountryInfo(CallingCode = 1,    CallingSubCode = ["246"], Alpha3 = "BRB", ShortName = "Barbados", LongName = "Barbados", CurrencyCode = "BBD"
               , Languages = ["en"], UnofficialNames = ["Barbade", "Barbados", "バルバドス", "巴巴多斯"], Timezones = ["America/Barbados,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.巴巴多斯))]
    BB = 052

   ,

    /// <summary>
    ///     巴哈马
    /// </summary>
    [CountryInfo(CallingCode = 1,      CallingSubCode = ["242"], Alpha3 = "BHS", ShortName = "Bahamas", LongName = "The Commonwealth of The Bahamas"
               , CurrencyCode = "BSD", Languages = ["en"], UnofficialNames = ["The Bahamas", "バハマ", "巴哈马"], Timezones = ["America/Nassau,-0500"])]
    [ResourceDescription<Ln>(nameof(Ln.巴哈马))]
    BS = 044

   ,

    /// <summary>
    ///     加拿大
    /// </summary>
    [CountryInfo(CallingCode = 1, Alpha3 = "CAN", ShortName = "Canada", LongName = "Canada", CurrencyCode = "CAD", Languages = ["en", "fr"]
               , UnofficialNames = ["Canada", "Kanada", "Canadá", "カナダ", "加拿大"]
               , Timezones = [
                     "America/Atikokan,-0500", "America/Blanc-Sablon,-0400", "America/Cambridge_Bay,-0700", "America/Creston,-0700"
                   , "America/Dawson,-0700", "America/Dawson_Creek,-0700", "America/Edmonton,-0700", "America/Fort_Nelson,-0700"
                   , "America/Glace_Bay,-0400", "America/Goose_Bay,-0400", "America/Halifax,-0400", "America/Inuvik,-0700", "America/Iqaluit,-0500"
                   , "America/Moncton,-0400", "America/Rankin_Inlet,-0600", "America/Regina,-0600", "America/Resolute,-0600", "America/St_Johns,-0330"
                   , "America/Swift_Current,-0600", "America/Toronto,-0500", "America/Vancouver,-0800", "America/Whitehorse,-0700"
                   , "America/Winnipeg,-0600"
                 ])]
    [ResourceDescription<Ln>(nameof(Ln.加拿大))]
    CA = 124

   ,

    /// <summary>
    ///     美国本土外小岛屿
    /// </summary>
    [CountryInfo(CallingCode = 1,                                   Alpha3 = "UMI",       ShortName = "United States Minor Outlying Islands"
               , LongName = "United States Minor Outlying Islands", CurrencyCode = "USD", Languages = ["en"]
               , UnofficialNames = [
                     "United States Minor Outlying Islands", "US-Amerikanische Hoheitsgebiete", "Dépendances américaines"
                   , "Islas menores de Estados Unidos", "合衆国領有小離島", "Kleine afgelegen eilanden van de Verenigde Staten", "美国本土外小岛屿"
                 ], Timezones = ["Pacific/Midway,-1100", "Pacific/Wake,+1200"])]
    [ResourceDescription<Ln>(nameof(Ln.美国本土外小岛屿))]
    UM = 581

   ,

    /// <summary>
    ///     美国
    /// </summary>
    [CountryInfo(CallingCode = 1,      Alpha3 = "USA", ShortName = "United States of America", LongName = "The United States of America"
               , CurrencyCode = "USD", Languages = ["en"]
               , UnofficialNames = [
                     "United States", "USA", "Vereinigte Staaten von Amerika", "États-Unis", "Estados Unidos", "アメリカ合衆国", "Verenigde Staten"
                   , "Соединенные Штаты Америки", "美国"
                 ], IsPreferred = true
            ,       Timezones = [
                     "America/Adak,-1000", "America/Anchorage,-0900", "America/Boise,-0700", "America/Chicago,-0600", "America/Denver,-0700"
                   , "America/Detroit,-0500", "America/Indiana/Indianapolis,-0500", "America/Indiana/Knox,-0600", "America/Indiana/Marengo,-0500"
                   , "America/Indiana/Petersburg,-0500", "America/Indiana/Tell_City,-0600", "America/Indiana/Vevay,-0500"
                   , "America/Indiana/Vincennes,-0500", "America/Indiana/Winamac,-0500", "America/Juneau,-0900", "America/Kentucky/Louisville,-0500"
                   , "America/Kentucky/Monticello,-0500", "America/Los_Angeles,-0800", "America/Menominee,-0600", "America/Metlakatla,-0900"
                   , "America/New_York,-0500", "America/Nome,-0900", "America/North_Dakota/Beulah,-0600", "America/North_Dakota/Center,-0600"
                   , "America/North_Dakota/New_Salem,-0600", "America/Phoenix,-0700", "America/Sitka,-0900", "America/Yakutat,-0900"
                   , "Pacific/Honolulu,-1000"
                 ])]
    [ResourceDescription<Ln>(nameof(Ln.美国))]
    US = 840

   ,

    /// <summary>
    ///     哈萨克斯坦
    /// </summary>
    [CountryInfo(CallingCode = 7,      CallingSubCode = ["6", "7"], Alpha3 = "KAZ", ShortName = "Kazakhstan", LongName = "The Republic of Kazakhstan"
               , CurrencyCode = "KZT", Languages = ["kk", "ru"]
               , UnofficialNames = ["Kazakhstan", "Kasachstan", "Kazajistán", "カザフスタン", "Kazachstan", "哈萨克斯坦"]
               , Timezones = [
                     "Asia/Almaty,+0500", "Asia/Aqtau,+0500", "Asia/Aqtobe,+0500", "Asia/Atyrau,+0500", "Asia/Oral,+0500", "Asia/Qostanay,+0500"
                   , "Asia/Qyzylorda,+0500"
                 ])]
    [ResourceDescription<Ln>(nameof(Ln.哈萨克斯坦))]
    KZ = 398

   ,

    /// <summary>
    ///     俄罗斯
    /// </summary>
    [CountryInfo(CallingCode = 7,    Alpha3 = "RUS", ShortName = "Russian Federation", LongName = "The Russian Federation", CurrencyCode = "RUB"
               , Languages = ["ru"], UnofficialNames = ["Russia", "Russland", "Russie", "Rusia", "ロシア連邦", "Rusland", "Россия", "Расія", "俄罗斯"]
               , IsPreferred = true
               , Timezones = [
                     "Asia/Anadyr,+1200", "Asia/Barnaul,+0700", "Asia/Chita,+0900", "Asia/Irkutsk,+0800", "Asia/Kamchatka,+1200"
                   , "Asia/Khandyga,+0900", "Asia/Krasnoyarsk,+0700", "Asia/Magadan,+1100", "Asia/Novokuznetsk,+0700", "Asia/Novosibirsk,+0700"
                   , "Asia/Omsk,+0600", "Asia/Sakhalin,+1100", "Asia/Srednekolymsk,+1100", "Asia/Tomsk,+0700", "Asia/Ust-Nera,+1000"
                   , "Asia/Vladivostok,+1000", "Asia/Yakutsk,+0900", "Asia/Yekaterinburg,+0500", "Europe/Astrakhan,+0400", "Europe/Kaliningrad,+0200"
                   , "Europe/Kirov,+0300", "Europe/Moscow,+0300", "Europe/Samara,+0400", "Europe/Saratov,+0400", "Europe/Ulyanovsk,+0400"
                   , "Europe/Volgograd,+0300"
                 ])]
    [ResourceDescription<Ln>(nameof(Ln.俄罗斯))]
    RU = 643

   ,

    /// <summary>
    ///     埃及
    /// </summary>
    [CountryInfo(CallingCode = 20,   Alpha3 = "EGY", ShortName = "Egypt", LongName = "The Arab Republic of Egypt", CurrencyCode = "EGP"
               , Languages = ["ar"], UnofficialNames = ["Egypt", "مصر", "Ägypten", "Égypte", "Egipto", "エジプト", "Egypte", "埃及"]
               , Timezones = ["Africa/Cairo,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.埃及))]
    EG = 818

   ,

    /// <summary>
    ///     南非
    /// </summary>
    [CountryInfo(CallingCode = 27, Alpha3 = "ZAF", ShortName = "South Africa", LongName = "The Republic of South Africa", CurrencyCode = "ZAR"
               , Languages = ["af", "en", "nr", "st", "ss", "tn", "ts", "ve", "xh", "zu"]
               , UnofficialNames = ["South Africa", "Republik Südafrika", "Afrique du Sud", "República de Sudáfrica", "南アフリカ", "Zuid-Afrika", "南非"]
               , Timezones = ["Africa/Johannesburg,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.南非))]
    ZA = 710

   ,

    /// <summary>
    ///     希腊
    /// </summary>
    [CountryInfo(CallingCode = 30, Alpha3 = "GRC", ShortName = "Greece", LongName = "The Hellenic Republic", CurrencyCode = "EUR", Languages = ["el"]
               , UnofficialNames = ["Greece", "Griechenland", "Grèce", "Grecia", "ギリシャ", "Griekenland", "希腊"], Timezones = ["Europe/Athens,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.希腊))]
    GR = 300

   ,

    /// <summary>
    ///     荷兰
    /// </summary>
    [CountryInfo(CallingCode = 31, Alpha3 = "NLD", ShortName = "Netherlands", LongName = "The Kingdom of the Netherlands", CurrencyCode = "EUR"
               , Languages = ["nl", "fy"]
               , UnofficialNames = [
                     "Netherlands", "The Netherlands", "Niederlande", "Pays-Bas", "Países Bajos", "オランダ", "Nederland", "Нидерландия", "荷兰"
                 ], Timezones = ["Europe/Amsterdam,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.荷兰))]
    NL = 528

   ,

    /// <summary>
    ///     比利时
    /// </summary>
    [CountryInfo(CallingCode = 32,               Alpha3 = "BEL", ShortName = "Belgium", LongName = "The Kingdom of Belgium", CurrencyCode = "EUR"
               , Languages = ["nl", "fr", "de"], UnofficialNames = ["Belgium", "Belgien", "Belgique", "Bélgica", "ベルギー", "België", "比利时"]
               , Timezones = ["Europe/Brussels,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.比利时))]
    BE = 056

   ,

    /// <summary>
    ///     法国
    /// </summary>
    [CountryInfo(CallingCode = 33, Alpha3 = "FRA", ShortName = "France", LongName = "The French Republic", CurrencyCode = "EUR", Languages = ["fr"]
               , UnofficialNames = ["France", "Frankreich", "the French Republic", "フランス", "Frankrijk", "Francia", "法国"]
               , Timezones = ["Europe/Paris,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.法国))]
    FR = 250

   ,

    /// <summary>
    ///     西班牙
    /// </summary>
    [CountryInfo(CallingCode = 34, Alpha3 = "ESP", ShortName = "Spain", LongName = "The Kingdom of Spain", CurrencyCode = "EUR", Languages = ["es"]
               , UnofficialNames = ["Spain", "Spanien", "Espagne", "España", "スペイン", "Spanje", "西班牙"]
               , Timezones = ["Africa/Ceuta,+0100", "Atlantic/Canary,+0000", "Europe/Madrid,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.西班牙))]
    ES = 724

   ,

    /// <summary>
    ///     匈牙利
    /// </summary>
    [CountryInfo(CallingCode = 36, Alpha3 = "HUN", ShortName = "Hungary", LongName = "Hungary", CurrencyCode = "HUF", Languages = ["hu"]
               , UnofficialNames = ["Hungary", "Ungarn", "Hongrie", "Hungría", "ハンガリー", "Hongarije", "匈牙利"], Timezones = ["Europe/Budapest,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.匈牙利))]
    HU = 348

   ,

    /// <summary>
    ///     意大利
    /// </summary>
    [CountryInfo(CallingCode = 39, Alpha3 = "ITA", ShortName = "Italy", LongName = "The Italian Republic", CurrencyCode = "EUR", Languages = ["it"]
               , UnofficialNames = ["Italy", "Italien", "Italie", "Italia", "イタリア", "Italië", "意大利"], IsPreferred = true
               , Timezones = ["Europe/Rome,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.意大利))]
    IT = 380

   ,

    /// <summary>
    ///     罗马尼亚
    /// </summary>
    [CountryInfo(CallingCode = 40, Alpha3 = "ROU", ShortName = "Romania", LongName = "Romania", CurrencyCode = "RON", Languages = ["ro"]
               , UnofficialNames = ["Romania", "Rumänien", "Roumanie", "Rumania", "ルーマニア", "Roemenië", "罗马尼亚"]
               , Timezones = ["Europe/Bucharest,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.罗马尼亚))]
    RO = 642

   ,

    /// <summary>
    ///     瑞士
    /// </summary>
    [CountryInfo(CallingCode = 41,               Alpha3 = "CHE", ShortName = "Switzerland", LongName = "The Swiss Confederation", CurrencyCode = "CHF"
               , Languages = ["de", "fr", "it"], UnofficialNames = ["Switzerland", "Schweiz", "Suisse", "Suiza", "スイス", "Zwitserland", "瑞士"]
               , Timezones = ["Europe/Zurich,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.瑞士))]
    CH = 756

   ,

    /// <summary>
    ///     奥地利
    /// </summary>
    [CountryInfo(CallingCode = 43,   Alpha3 = "AUT", ShortName = "Austria", LongName = "The Republic of Austria", CurrencyCode = "EUR"
               , Languages = ["de"], UnofficialNames = ["Austria", "Österreich", "Autriche", "オーストリア", "Oostenrijk", "奥地利"]
               , Timezones = ["Europe/Vienna,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.奥地利))]
    AT = 040

   ,

    /// <summary>
    ///     英国
    /// </summary>
    [CountryInfo(CallingCode = 44, Alpha3 = "GBR", ShortName = "United Kingdom of Great Britain and Northern Ireland"
               , LongName = "The United Kingdom of Great Britain and Northern Ireland", CurrencyCode = "GBP", Languages = ["en"]
               , UnofficialNames = [
                     "United Kingdom", "The United Kingdom", "England", "Großbritannien", "Vereinigtes Königreich", "Royaume-Uni", "Reino Unido"
                   , "イギリス", "Verenigd Koninkrijk", "Great Britain (UK)", "UK", "Великобритания", "Velká Británie", "İngiltere", "Великобританія"
                   , "英国"
                 ], IsPreferred = true, Timezones = ["Europe/London,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.英国))]
    GB = 826

   ,

    /// <summary>
    ///     根西
    /// </summary>
    [CountryInfo(CallingCode = 44,     CallingSubCode = ["1481"], Alpha3 = "GGY", ShortName = "Guernsey", LongName = "The Bailiwick of Guernsey"
               , CurrencyCode = "GBP", Languages = ["en", "fr"]
               , UnofficialNames = [
                     "Guernsey and Alderney", "Guernsey und Alderney", "Guernsey et Alderney", "Guernsey y Alderney", "ガーンジー", "Guernsey", "根西"
                 ], Timezones = ["Europe/Guernsey,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.根西))]
    GG = 831

   ,

    /// <summary>
    ///     马恩岛
    /// </summary>
    [CountryInfo(CallingCode = 44, CallingSubCode = ["1624"], Alpha3 = "IMN", ShortName = "Isle of Man", LongName = "The Isle of Man"
               , CurrencyCode = "GBP", Languages = ["en", "gv"]
               , UnofficialNames = ["Isle of Man", "Insel Man", "Île de Man", "Isla de Man", "マン島", "马恩岛"], Timezones = ["Europe/Isle_of_Man,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.马恩岛))]
    IM = 833

   ,

    /// <summary>
    ///     泽西
    /// </summary>
    [CountryInfo(CallingCode = 44,     CallingSubCode = ["1534"], Alpha3 = "JEY", ShortName = "Jersey", LongName = "The Bailiwick of Jersey"
               , CurrencyCode = "GBP", Languages = ["en", "fr"],  UnofficialNames = ["Jersey", "ジャージー", "泽西"], Timezones = ["Europe/Jersey,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.泽西))]
    JE = 832

   ,

    /// <summary>
    ///     丹麦
    /// </summary>
    [CountryInfo(CallingCode = 45,   Alpha3 = "DNK", ShortName = "Denmark", LongName = "The Kingdom of Denmark", CurrencyCode = "DKK"
               , Languages = ["da"], UnofficialNames = ["Denmark", "Dänemark", "Danemark", "Dinamarca", "デンマーク", "Denemarken", "丹麦"]
               , Timezones = ["Europe/Copenhagen,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.丹麦))]
    DK = 208

   ,

    /// <summary>
    ///     瑞典
    /// </summary>
    [CountryInfo(CallingCode = 46, Alpha3 = "SWE", ShortName = "Sweden", LongName = "The Kingdom of Sweden", CurrencyCode = "SEK", Languages = ["sv"]
               , UnofficialNames = ["Sweden", "Schweden", "Suède", "Suecia", "スウェーデン", "Zweden", "瑞典"], Timezones = ["Europe/Stockholm,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.瑞典))]
    SE = 752

   ,

    /// <summary>
    ///     斯瓦尔巴和扬马延
    /// </summary>
    [CountryInfo(CallingCode = 47, CallingSubCode = ["79"], Alpha3 = "SJM", ShortName = "Svalbard and Jan Mayen", LongName = "Svalbard and Jan Mayen"
               , CurrencyCode = "NOK", Languages = ["no"]
               , UnofficialNames = [
                     "Svalbard and Jan Mayen", "Svalbard und Jan Mayen", "Îles Svalbard et Jan Mayen", "Islas Svalbard y Jan Mayen"
                   , "スヴァールバル諸島およびヤンマイエン島", "Svalbard en Jan Mayen", "斯瓦尔巴和扬马延"
                 ], Timezones = ["Arctic/Longyearbyen,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.斯瓦尔巴和扬马延))]
    SJ = 744

   ,

    /// <summary>
    ///     布韦岛
    /// </summary>
    [CountryInfo(CallingCode = 47, Alpha3 = "BVT", ShortName = "Bouvet Island", LongName = "Bouvet Island", CurrencyCode = "NOK", Languages = []
               , UnofficialNames = ["Bouvet Island", "Bouvetinsel", "ブーベ島", "Bouveteiland", "布韦岛"])]
    [ResourceDescription<Ln>(nameof(Ln.布韦岛))]
    BV = 074

   ,

    /// <summary>
    ///     挪威
    /// </summary>
    [CountryInfo(CallingCode = 47,         Alpha3 = "NOR", ShortName = "Norway", LongName = "The Kingdom of Norway", CurrencyCode = "NOK"
               , Languages = ["nb", "nn"], UnofficialNames = ["Norway", "Norwegen", "Norvège", "Noruega", "ノルウェー", "Noorwegen", "挪威"]
               , IsPreferred = true,       Timezones = ["Europe/Oslo,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.挪威))]
    NO = 578

   ,

    /// <summary>
    ///     波兰
    /// </summary>
    [CountryInfo(CallingCode = 48, Alpha3 = "POL", ShortName = "Poland", LongName = "The Republic of Poland", CurrencyCode = "PLN", Languages = ["pl"]
               , UnofficialNames = ["Poland", "Polen", "Pologne", "Polonia", "ポーランド", "波兰"], Timezones = ["Europe/Warsaw,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.波兰))]
    PL = 616

   ,

    /// <summary>
    ///     德国
    /// </summary>
    [CountryInfo(CallingCode = 49,   Alpha3 = "DEU", ShortName = "Germany", LongName = "The Federal Republic of Germany", CurrencyCode = "EUR"
               , Languages = ["de"], UnofficialNames = ["Germany", "Deutschland", "Allemagne", "Alemania", "ドイツ", "Duitsland", "德国"]
               , Timezones = ["Europe/Berlin,+0100", "Europe/Busingen,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.德国))]
    DE = 276

   ,

    /// <summary>
    ///     秘鲁
    /// </summary>
    [CountryInfo(CallingCode = 51, Alpha3 = "PER", ShortName = "Peru", LongName = "The Republic of Perú", CurrencyCode = "PEN", Languages = ["es"]
               , UnofficialNames = ["Peru", "Pérou", "Perú", "ペルー", "秘鲁"], Timezones = ["America/Lima,-0500"])]
    [ResourceDescription<Ln>(nameof(Ln.秘鲁))]
    PE = 604

   ,

    /// <summary>
    ///     墨西哥
    /// </summary>
    [CountryInfo(CallingCode = 52,   Alpha3 = "MEX", ShortName = "Mexico", LongName = "The United Mexican States", CurrencyCode = "MXN"
               , Languages = ["es"], UnofficialNames = ["Mexico", "Mexiko", "Mexique", "México", "メキシコ", "墨西哥"]
               , Timezones = [
                     "America/Bahia_Banderas,-0600", "America/Cancun,-0500", "America/Chihuahua,-0600", "America/Ciudad_Juarez,-0700"
                   , "America/Hermosillo,-0700", "America/Matamoros,-0600", "America/Mazatlan,-0700", "America/Merida,-0600"
                   , "America/Mexico_City,-0600", "America/Monterrey,-0600", "America/Ojinaga,-0600", "America/Tijuana,-0800"
                 ])]
    [ResourceDescription<Ln>(nameof(Ln.墨西哥))]
    MX = 484

   ,

    /// <summary>
    ///     古巴
    /// </summary>
    [CountryInfo(CallingCode = 53, Alpha3 = "CUB", ShortName = "Cuba", LongName = "The Republic of Cuba", CurrencyCode = "CUP", Languages = ["es"]
               , UnofficialNames = ["Cuba", "Kuba", "キューバ", "古巴"], Timezones = ["America/Havana,-0500"])]
    [ResourceDescription<Ln>(nameof(Ln.古巴))]
    CU = 192

   ,

    /// <summary>
    ///     阿根廷
    /// </summary>
    [CountryInfo(CallingCode = 54,         Alpha3 = "ARG", ShortName = "Argentina", LongName = "The Argentine Republic", CurrencyCode = "ARS"
               , Languages = ["es", "gn"], UnofficialNames = ["Argentina", "Argentinien", "Argentine", "アルゼンチン", "Argentinië", "阿根廷"]
               , Timezones = [
                     "America/Argentina/Buenos_Aires,-0300", "America/Argentina/Catamarca,-0300", "America/Argentina/Cordoba,-0300"
                   , "America/Argentina/Jujuy,-0300", "America/Argentina/La_Rioja,-0300", "America/Argentina/Mendoza,-0300"
                   , "America/Argentina/Rio_Gallegos,-0300", "America/Argentina/Salta,-0300", "America/Argentina/San_Juan,-0300"
                   , "America/Argentina/San_Luis,-0300", "America/Argentina/Tucuman,-0300", "America/Argentina/Ushuaia,-0300"
                 ])]
    [ResourceDescription<Ln>(nameof(Ln.阿根廷))]
    AR = 032

   ,

    /// <summary>
    ///     巴西
    /// </summary>
    [CountryInfo(CallingCode = 55,   Alpha3 = "BRA", ShortName = "Brazil", LongName = "The Federative Republic of Brazil", CurrencyCode = "BRL"
               , Languages = ["pt"], UnofficialNames = ["Brazil", "Brasilien", "Brésil", "Brasil", "ブラジル", "Brazilië", "巴西"]
               , Timezones = [
                     "America/Araguaina,-0300", "America/Bahia,-0300", "America/Belem,-0300", "America/Boa_Vista,-0400", "America/Campo_Grande,-0400"
                   , "America/Cuiaba,-0400", "America/Eirunepe,-0500", "America/Fortaleza,-0300", "America/Maceio,-0300", "America/Manaus,-0400"
                   , "America/Noronha,-0200", "America/Porto_Velho,-0400", "America/Recife,-0300", "America/Rio_Branco,-0500"
                   , "America/Santarem,-0300", "America/Sao_Paulo,-0300"
                 ])]
    [ResourceDescription<Ln>(nameof(Ln.巴西))]
    BR = 076

   ,

    /// <summary>
    ///     智利
    /// </summary>
    [CountryInfo(CallingCode = 56, Alpha3 = "CHL", ShortName = "Chile", LongName = "The Republic of Chile", CurrencyCode = "CLP", Languages = ["es"]
               , UnofficialNames = ["Chile", "チリ", "Chili", "智利"]
               , Timezones = ["America/Punta_Arenas,-0300", "America/Santiago,-0300", "Pacific/Easter,-0500"])]
    [ResourceDescription<Ln>(nameof(Ln.智利))]
    CL = 152

   ,

    /// <summary>
    ///     哥伦比亚
    /// </summary>
    [CountryInfo(CallingCode = 57,   Alpha3 = "COL", ShortName = "Colombia", LongName = "The Republic of Colombia", CurrencyCode = "COP"
               , Languages = ["es"], UnofficialNames = ["Colombia", "Kolumbien", "Colombie", "コロンビア", "哥伦比亚"], Timezones = ["America/Bogota,-0500"])]
    [ResourceDescription<Ln>(nameof(Ln.哥伦比亚))]
    CO = 170

   ,

    /// <summary>
    ///     委内瑞拉
    /// </summary>
    [CountryInfo(CallingCode = 58, Alpha3 = "VEN", ShortName = "Venezuela (Bolivarian Republic of)", LongName = "The Bolivarian Republic of Venezuela"
               , CurrencyCode = "VES", Languages = ["es"], UnofficialNames = ["Venezuela", "ベネズエラ・ボリバル共和国", "委内瑞拉"]
               , Timezones = ["America/Caracas,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.委内瑞拉))]
    VE = 862

   ,

    /// <summary>
    ///     马来西亚
    /// </summary>
    [CountryInfo(CallingCode = 60, Alpha3 = "MYS", ShortName = "Malaysia", LongName = "Malaysia", CurrencyCode = "MYR", Languages = ["ms", "en"]
               , UnofficialNames = ["Malaysia", "Malaisie", "Malasia", "マレーシア", "Maleisië", "马来西亚"]
               , Timezones = ["Asia/Kuala_Lumpur,+0800", "Asia/Kuching,+0800"])]
    [ResourceDescription<Ln>(nameof(Ln.马来西亚))]
    MY = 458

   ,

    /// <summary>
    ///     澳大利亚
    /// </summary>
    [CountryInfo(CallingCode = 61,   Alpha3 = "AUS", ShortName = "Australia", LongName = "The Commonwealth of Australia", CurrencyCode = "AUD"
               , Languages = ["en"], UnofficialNames = ["Australia", "Australien", "Australie", "オーストラリア", "Australië", "澳洲", "澳大利亚"]
               , IsPreferred = true
               , Timezones = [
                     "Antarctica/Macquarie,+1100", "Australia/Adelaide,+1030", "Australia/Brisbane,+1000", "Australia/Broken_Hill,+1030"
                   , "Australia/Darwin,+0930", "Australia/Eucla,+0845", "Australia/Hobart,+1100", "Australia/Lindeman,+1000"
                   , "Australia/Lord_Howe,+1100", "Australia/Melbourne,+1100", "Australia/Perth,+0800", "Australia/Sydney,+1100"
                 ])]
    [ResourceDescription<Ln>(nameof(Ln.澳大利亚))]
    AU = 036

   ,

    /// <summary>
    ///     科科斯基林群岛
    /// </summary>
    [CountryInfo(CallingCode = 61, CallingSubCode = ["89162"], Alpha3 = "CCK", ShortName = "Cocos (Keeling) Islands"
               , LongName = "The Territory of Cocos (Keeling) Islands", CurrencyCode = "AUD", Languages = ["en"]
               , UnofficialNames = ["Cocos (Keeling) Islands", "Kokosinseln", "ココス（キーリング）諸島", "Cocoseilanden", "科科斯基林群岛"]
               , Timezones = ["Indian/Cocos,+0630"])]
    [ResourceDescription<Ln>(nameof(Ln.科科斯基林群岛))]
    CC = 166

   ,

    /// <summary>
    ///     圣诞岛
    /// </summary>
    [CountryInfo(CallingCode = 61,                               CallingSubCode = ["89164"], Alpha3 = "CXR", ShortName = "Christmas Island"
               , LongName = "The Territory of Christmas Island", CurrencyCode = "AUD",       Languages = ["en", "zh", "ms"]
               , UnofficialNames = ["Christmas Island", "Weihnachtsinsel", "クリスマス島", "Christmaseiland", "圣诞岛"]
               , Timezones = ["Indian/Christmas,+0700"])]
    [ResourceDescription<Ln>(nameof(Ln.圣诞岛))]
    CX = 162

   ,

    /// <summary>
    ///     赫德岛和麦克唐纳群岛
    /// </summary>
    [CountryInfo(CallingCode = 672, CallingSubCode = ["1"], Alpha3 = "HMD", ShortName = "Heard Island and McDonald Islands"
               , LongName = "The Territory of Heard Island and McDonald Islands", CurrencyCode = "AUD", Languages = ["en"]
               , UnofficialNames = [
                     "Heard and McDonald Islands", "Heard und die McDonaldinseln", "ハード島とマクドナルド諸島", "Heard- en McDonaldeilanden"
                   , "Heard Island and McDonald Islands", "赫德岛和麦克唐纳群岛"
                 ])]
    [ResourceDescription<Ln>(nameof(Ln.赫德岛和麦克唐纳群岛))]
    HM = 334

   ,

    /// <summary>
    ///     印度尼西亚
    /// </summary>
    [CountryInfo(CallingCode = 62,   Alpha3 = "IDN", ShortName = "Indonesia", LongName = "The Republic of Indonesia", CurrencyCode = "IDR"
               , Languages = ["id"], UnofficialNames = ["Indonesia", "Indonesien", "Indonésie", "インドネシア", "Indonesië", "印度尼西亚"]
               , Timezones = ["Asia/Jakarta,+0700", "Asia/Jayapura,+0900", "Asia/Makassar,+0800", "Asia/Pontianak,+0700"])]
    [ResourceDescription<Ln>(nameof(Ln.印度尼西亚))]
    ID = 360

   ,

    /// <summary>
    ///     菲律宾
    /// </summary>
    [CountryInfo(CallingCode = 63, Alpha3 = "PHL", ShortName = "Philippines", LongName = "The Republic of the Philippines", CurrencyCode = "PHP"
               , Languages = ["tl", "en"], UnofficialNames = ["Philippines", "Philippinen", "Filipinas", "フィリピン", "Filipijnen", "菲律宾"]
               , Timezones = ["Asia/Manila,+0800"])]
    [ResourceDescription<Ln>(nameof(Ln.菲律宾))]
    PH = 608

   ,

    /// <summary>
    ///     新西兰
    /// </summary>
    [CountryInfo(CallingCode = 64, Alpha3 = "NZL", ShortName = "New Zealand", LongName = "New Zealand", CurrencyCode = "NZD", Languages = ["en"]
               , UnofficialNames = ["New Zealand", "Neuseeland", "Nouvelle Zélande", "Nueva Zelanda", "ニュージーランド", "Nieuw-Zeeland", "新西兰"]
               , IsPreferred = true, Timezones = ["Pacific/Auckland,+1300", "Pacific/Chatham,+1345"])]
    [ResourceDescription<Ln>(nameof(Ln.新西兰))]
    NZ = 554

   ,

    /// <summary>
    ///     皮特凯恩群岛
    /// </summary>
    [CountryInfo(CallingCode = 64,     Alpha3 = "PCN",     ShortName = "Pitcairn", LongName = "The Pitcairn, Henderson, Ducie and Oeno Islands"
               , CurrencyCode = "NZD", Languages = ["en"], UnofficialNames = ["Pitcairn", "ピトケアン", "Pitcairneilanden", "Pitcairn Islands", "皮特凯恩群岛"]
               , Timezones = ["Pacific/Pitcairn,-0800"])]
    [ResourceDescription<Ln>(nameof(Ln.皮特凯恩群岛))]
    PN = 612

   ,

    /// <summary>
    ///     新加坡
    /// </summary>
    [CountryInfo(CallingCode = 65,               Alpha3 = "SGP", ShortName = "Singapore", LongName = "The Republic of Singapore", CurrencyCode = "SGD"
               , Languages = ["en", "ms", "ta"], UnofficialNames = ["Singapore", "Singapur", "Singapour", "シンガポール", "新加坡"]
               , Timezones = ["Asia/Singapore,+0800"])]
    [ResourceDescription<Ln>(nameof(Ln.新加坡))]
    SG = 702

   ,

    /// <summary>
    ///     泰国
    /// </summary>
    [CountryInfo(CallingCode = 66,   Alpha3 = "THA", ShortName = "Thailand", LongName = "The Kingdom of Thailand", CurrencyCode = "THB"
               , Languages = ["th"], UnofficialNames = ["Thailand", "Thaïlande", "Tailandia", "タイ", "ประเทศไทย", "泰国"]
               , Timezones = ["Asia/Bangkok,+0700"])]
    [ResourceDescription<Ln>(nameof(Ln.泰国))]
    TH = 764

   ,

    /// <summary>
    ///     日本
    /// </summary>
    [CountryInfo(CallingCode = 81, Alpha3 = "JPN", ShortName = "Japan", LongName = "Japan", CurrencyCode = "JPY", Languages = ["ja"]
               , UnofficialNames = ["Japan", "Japon", "Japón", "日本"], Timezones = ["Asia/Tokyo,+0900"])]
    [ResourceDescription<Ln>(nameof(Ln.日本))]
    JP = 392

   ,

    /// <summary>
    ///     韩国
    /// </summary>
    [CountryInfo(CallingCode = 82, Alpha3 = "KOR", ShortName = "Korea (Republic of)", LongName = "The Republic of Korea", CurrencyCode = "KRW"
               , Languages = ["ko"]
               , UnofficialNames = [
                     "South Korea", "Korea (South)", "Südkorea", "Corée du Sud", "Corea del Sur", "大韓民国", "Zuid-Korea", "Korea (Republic of)", "韩国"
                 ], Timezones = ["Asia/Seoul,+0900"])]
    [ResourceDescription<Ln>(nameof(Ln.韩国))]
    KR = 410

   ,

    /// <summary>
    ///     越南
    /// </summary>
    [CountryInfo(CallingCode = 84,   Alpha3 = "VNM", ShortName = "Viet Nam", LongName = "The Socialist Republic of Viet Nam", CurrencyCode = "VND"
               , Languages = ["vi"], UnofficialNames = ["Vietnam", "ベトナム", "Viet Nam", "越南"], Timezones = ["Asia/Ho_Chi_Minh,+0700"])]
    [ResourceDescription<Ln>(nameof(Ln.越南))]
    VN = 704

   ,

    /// <summary>
    ///     中国
    /// </summary>
    [CountryInfo(CallingCode = 86,   Alpha3 = "CHN", ShortName = "China", LongName = "The People's Republic of China", CurrencyCode = "CNY"
               , Languages = ["zh"], UnofficialNames = ["China", "Chine", "中国"], Timezones = ["Asia/Shanghai,+0800", "Asia/Urumqi,+0600"])]
    [ResourceDescription<Ln>(nameof(Ln.中国))]
    CN = 156

   ,

    /// <summary>
    ///     土耳其
    /// </summary>
    [CountryInfo(CallingCode = 90,   Alpha3 = "TUR", ShortName = "Türkiye", LongName = "The Republic of Türkiye", CurrencyCode = "TRY"
               , Languages = ["tr"], UnofficialNames = ["Turkey", "Türkei", "Turquie", "Turquía", "トルコ", "Turkije", "土耳其"]
               , Timezones = ["Europe/Istanbul,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.土耳其))]
    TR = 792

   ,

    /// <summary>
    ///     印度
    /// </summary>
    [CountryInfo(CallingCode = 91,         Alpha3 = "IND", ShortName = "India", LongName = "The Republic of India", CurrencyCode = "INR"
               , Languages = ["hi", "en"], UnofficialNames = ["India", "Indien", "Inde", "インド", "印度"], Timezones = ["Asia/Kolkata,+0530"])]
    [ResourceDescription<Ln>(nameof(Ln.印度))]
    IN = 356

   ,

    /// <summary>
    ///     巴基斯坦
    /// </summary>
    [CountryInfo(CallingCode = 92,         Alpha3 = "PAK", ShortName = "Pakistan", LongName = "The Islamic Republic of Pakistan", CurrencyCode = "PKR"
               , Languages = ["en", "ur"], UnofficialNames = ["Pakistan", "Paquistán", "パキスタン", "巴基斯坦"], Timezones = ["Asia/Karachi,+0500"])]
    [ResourceDescription<Ln>(nameof(Ln.巴基斯坦))]
    PK = 586

   ,

    /// <summary>
    ///     阿富汗
    /// </summary>
    [CountryInfo(CallingCode = 93, Alpha3 = "AFG", ShortName = "Afghanistan", LongName = "The Islamic Republic of Afghanistan", CurrencyCode = "AFN"
               , Languages = ["ps", "uz", "tk"], UnofficialNames = ["Afghanistan", "Afganistán", "アフガニスタン", "阿富汗"], Timezones = ["Asia/Kabul,+0430"])]
    [ResourceDescription<Ln>(nameof(Ln.阿富汗))]
    AF = 004

   ,

    /// <summary>
    ///     斯里兰卡
    /// </summary>
    [CountryInfo(CallingCode = 94,     Alpha3 = "LKA",           ShortName = "Sri Lanka", LongName = "The Democratic Socialist Republic of Sri Lanka"
               , CurrencyCode = "LKR", Languages = ["si", "ta"], UnofficialNames = ["Sri Lanka", "スリランカ", "斯里兰卡"]
               , Timezones = ["Asia/Colombo,+0530"])]
    [ResourceDescription<Ln>(nameof(Ln.斯里兰卡))]
    LK = 144

   ,

    /// <summary>
    ///     缅甸
    /// </summary>
    [CountryInfo(CallingCode = 95,   Alpha3 = "MMR", ShortName = "Myanmar", LongName = "The Republic of the Union of Myanmar", CurrencyCode = "MMK"
               , Languages = ["my"], UnofficialNames = ["Myanmar (Burma)", "ミャンマー", "缅甸"], Timezones = ["Asia/Yangon,+0630"])]
    [ResourceDescription<Ln>(nameof(Ln.缅甸))]
    MM = 104

   ,

    /// <summary>
    ///     伊朗
    /// </summary>
    [CountryInfo(CallingCode = 98,     Alpha3 = "IRN", ShortName = "Iran (Islamic Republic of)", LongName = "The Islamic Republic of Iran"
               , CurrencyCode = "IRR", Languages = ["fa"]
               , UnofficialNames = ["Iran", "Irán", "Iran (Islamic Republic Of)", "イラン・イスラム共和国", "Islamic Republic of Iran", "伊朗"]
               , Timezones = ["Asia/Tehran,+0330"])]
    [ResourceDescription<Ln>(nameof(Ln.伊朗))]
    IR = 364

   ,

    /// <summary>
    ///     南苏丹
    /// </summary>
    [CountryInfo(CallingCode = 211,        Alpha3 = "SSD", ShortName = "South Sudan", LongName = "The Republic of South Sudan", CurrencyCode = "SSP"
               , Languages = ["ar", "en"], UnofficialNames = ["South Sudan", "Südsudan", "南スーダン", "Zuid-Soedan", "南苏丹"]
               , Timezones = ["Africa/Juba,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.南苏丹))]
    SS = 728

   ,

    /// <summary>
    ///     西撒哈拉
    /// </summary>
    [CountryInfo(CallingCode = 212,    Alpha3 = "ESH", ShortName = "Western Sahara", LongName = "The Sahrawi Arab Democratic Republic"
               , CurrencyCode = "MAD", Languages = ["es", "fr"]
               , UnofficialNames = ["Western Sahara", "الصحراء الغربية", "Westsahara", "Sahara Occidental", "西サハラ", "Westelijke Sahara", "西撒哈拉"])]
    [ResourceDescription<Ln>(nameof(Ln.西撒哈拉))]
    EH = 732

   ,

    /// <summary>
    ///     摩洛哥
    /// </summary>
    [CountryInfo(CallingCode = 212,  Alpha3 = "MAR", ShortName = "Morocco", LongName = "The Kingdom of Morocco", CurrencyCode = "MAD"
               , Languages = ["ar"], UnofficialNames = ["Morocco", "المغرب", "Marokko", "Maroc", "Marruecos", "モロッコ", "摩洛哥"], IsPreferred = true
               , Timezones = ["Africa/Casablanca,+0100", "Africa/El_Aaiun,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.摩洛哥))]
    MA = 504

   ,

    /// <summary>
    ///     阿尔及利亚
    /// </summary>
    [CountryInfo(CallingCode = 213,    Alpha3 = "DZA", ShortName = "Algeria", LongName = "The People's Democratic Republic of Algeria"
               , CurrencyCode = "DZD", Languages = ["ar"]
               , UnofficialNames = ["Algeria", "الجزائر", "Algerien", "Algérie", "Argelia", "アルジェリア", "Algerije", "阿尔及利亚"]
               , Timezones = ["Africa/Algiers,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.阿尔及利亚))]
    DZ = 012

   ,

    /// <summary>
    ///     突尼斯
    /// </summary>
    [CountryInfo(CallingCode = 216,        Alpha3 = "TUN", ShortName = "Tunisia", LongName = "The Republic of Tunisia", CurrencyCode = "TND"
               , Languages = ["ar", "fr"], UnofficialNames = ["Tunisia", "تونس", "Tunesien", "Tunisie", "Túnez", "チュニジア", "Tunesië", "突尼斯"]
               , Timezones = ["Africa/Tunis,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.突尼斯))]
    TN = 788

   ,

    /// <summary>
    ///     利比亚
    /// </summary>
    [CountryInfo(CallingCode = 218, Alpha3 = "LBY", ShortName = "Libya", LongName = "The State of Libya", CurrencyCode = "LYD", Languages = ["ar"]
               , UnofficialNames = ["Libya", "ليبيا", "Libyen", "Libye", "Libia", "リビア", "Libië", "Libyan Arab Jamahiriya", "利比亚"]
               , Timezones = ["Africa/Tripoli,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.利比亚))]
    LY = 434

   ,

    /// <summary>
    ///     冈比亚
    /// </summary>
    [CountryInfo(CallingCode = 220,  Alpha3 = "GMB", ShortName = "Gambia", LongName = "The Republic of The Gambia", CurrencyCode = "GMD"
               , Languages = ["en"], UnofficialNames = ["The Gambia", "ガンビア", "冈比亚"], Timezones = ["Africa/Banjul,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.冈比亚))]
    GM = 270

   ,

    /// <summary>
    ///     塞内加尔
    /// </summary>
    [CountryInfo(CallingCode = 221,  Alpha3 = "SEN", ShortName = "Senegal", LongName = "The Republic of Senegal", CurrencyCode = "XOF"
               , Languages = ["fr"], UnofficialNames = ["Senegal", "Sénégal", "セネガル", "塞内加尔"], Timezones = ["Africa/Dakar,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.塞内加尔))]
    SN = 686

   ,

    /// <summary>
    ///     毛里塔尼亚
    /// </summary>
    [CountryInfo(CallingCode = 222, Alpha3 = "MRT", ShortName = "Mauritania", LongName = "The Islamic Republic of Mauritania", CurrencyCode = "MRU"
               , Languages = ["ar", "fr"], UnofficialNames = ["Mauritania", "موريتانيا", "Mauretanien", "Mauritanie", "モーリタニア", "Mauritanië", "毛里塔尼亚"]
               , Timezones = ["Africa/Nouakchott,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.毛里塔尼亚))]
    MR = 478

   ,

    /// <summary>
    ///     马里
    /// </summary>
    [CountryInfo(CallingCode = 223, Alpha3 = "MLI", ShortName = "Mali", LongName = "The Republic of Mali", CurrencyCode = "XOF", Languages = ["fr"]
               , UnofficialNames = ["Mali", "マリ", "马里"], Timezones = ["Africa/Bamako,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.马里))]
    ML = 466

   ,

    /// <summary>
    ///     几内亚
    /// </summary>
    [CountryInfo(CallingCode = 224,        Alpha3 = "GIN", ShortName = "Guinea", LongName = "The Republic of Guinea", CurrencyCode = "GNF"
               , Languages = ["fr", "ff"], UnofficialNames = ["Guinea", "Guinée", "ギニア", "Guinee", "几内亚"], Timezones = ["Africa/Conakry,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.几内亚))]
    GN = 324

   ,

    /// <summary>
    ///     科特迪瓦
    /// </summary>
    [CountryInfo(CallingCode = 225, Alpha3 = "CIV", ShortName = "Côte d'Ivoire", LongName = "The Republic of Côte d'Ivoire", CurrencyCode = "XOF"
               , Languages = ["fr"]
               , UnofficialNames = [
                     "Côte D'Ivoire", "Elfenbeinküste", "コートジボワール", "Ivoorkust", "Cote D'Ivoire (Ivory Coast)", "Cote d Ivoire (Ivory Coast)"
                   , "Ivory Coast", "科特迪瓦"
                 ], Timezones = ["Africa/Abidjan,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.科特迪瓦))]
    CI = 384

   ,

    /// <summary>
    ///     布基纳法索
    /// </summary>
    [CountryInfo(CallingCode = 226,        Alpha3 = "BFA", ShortName = "Burkina Faso", LongName = "Burkina Faso", CurrencyCode = "XOF"
               , Languages = ["fr", "ff"], UnofficialNames = ["Burkina Faso", "ブルキナファソ", "布基纳法索"], Timezones = ["Africa/Ouagadougou,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.布基纳法索))]
    BF = 854

   ,

    /// <summary>
    ///     尼日尔
    /// </summary>
    [CountryInfo(CallingCode = 227,  Alpha3 = "NER", ShortName = "Niger", LongName = "The Republic of the Niger", CurrencyCode = "XOF"
               , Languages = ["fr"], UnofficialNames = ["Niger", "Níger", "ニジェール", "尼日尔"], Timezones = ["Africa/Niamey,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.尼日尔))]
    NE = 562

   ,

    /// <summary>
    ///     多哥
    /// </summary>
    [CountryInfo(CallingCode = 228, Alpha3 = "TGO", ShortName = "Togo", LongName = "The Togolese Republic", CurrencyCode = "XOF", Languages = ["fr"]
               , UnofficialNames = ["Togo", "トーゴ", "多哥"], Timezones = ["Africa/Lome,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.多哥))]
    TG = 768

   ,

    /// <summary>
    ///     贝宁
    /// </summary>
    [CountryInfo(CallingCode = 229, Alpha3 = "BEN", ShortName = "Benin", LongName = "The Republic of Benin", CurrencyCode = "XOF", Languages = ["fr"]
               , UnofficialNames = ["Benin", "Bénin", "ベナン", "贝宁"], Timezones = ["Africa/Porto-Novo,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.贝宁))]
    BJ = 204

   ,

    /// <summary>
    ///     毛里求斯
    /// </summary>
    [CountryInfo(CallingCode = 230,  Alpha3 = "MUS", ShortName = "Mauritius", LongName = "The Republic of Mauritius", CurrencyCode = "MUR"
               , Languages = ["en"], UnofficialNames = ["Mauritius", "Île Maurice", "Mauricio", "モーリシャス", "毛里求斯"]
               , Timezones = ["Indian/Mauritius,+0400"])]
    [ResourceDescription<Ln>(nameof(Ln.毛里求斯))]
    MU = 480

   ,

    /// <summary>
    ///     利比里亚
    /// </summary>
    [CountryInfo(CallingCode = 231,  Alpha3 = "LBR", ShortName = "Liberia", LongName = "The Republic of Liberia", CurrencyCode = "LRD"
               , Languages = ["en"], UnofficialNames = ["Liberia", "リベリア", "利比里亚"], Timezones = ["Africa/Monrovia,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.利比里亚))]
    LR = 430

   ,

    /// <summary>
    ///     塞拉利昂
    /// </summary>
    [CountryInfo(CallingCode = 232,  Alpha3 = "SLE", ShortName = "Sierra Leone", LongName = "The Republic of Sierra Leone", CurrencyCode = "SLL"
               , Languages = ["en"], UnofficialNames = ["Sierra Leone", "シエラレオネ", "塞拉利昂"], Timezones = ["Africa/Freetown,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.塞拉利昂))]
    SL = 694

   ,

    /// <summary>
    ///     加纳
    /// </summary>
    [CountryInfo(CallingCode = 233, Alpha3 = "GHA", ShortName = "Ghana", LongName = "The Republic of Ghana", CurrencyCode = "GHS", Languages = ["en"]
               , UnofficialNames = ["Ghana", "ガーナ", "加纳"], Timezones = ["Africa/Accra,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.加纳))]
    GH = 288

   ,

    /// <summary>
    ///     尼日利亚
    /// </summary>
    [CountryInfo(CallingCode = 234,  Alpha3 = "NGA", ShortName = "Nigeria", LongName = "The Federal Republic of Nigeria", CurrencyCode = "NGN"
               , Languages = ["en"], UnofficialNames = ["Nigeria", "Nigéria", "the Federal Republic of Nigeria", "ナイジェリア", "尼日利亚"]
               , Timezones = ["Africa/Lagos,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.尼日利亚))]
    NG = 566

   ,

    /// <summary>
    ///     乍得
    /// </summary>
    [CountryInfo(CallingCode = 235,        Alpha3 = "TCD", ShortName = "Chad", LongName = "The Republic of Chad", CurrencyCode = "XAF"
               , Languages = ["ar", "fr"], UnofficialNames = ["Chad", "تشاد", "Tschad", "Tchad", "チャド", "Tsjaad", "乍得"]
               , Timezones = ["Africa/Ndjamena,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.乍得))]
    TD = 148

   ,

    /// <summary>
    ///     中非
    /// </summary>
    [CountryInfo(CallingCode = 236,    Alpha3 = "CAF", ShortName = "Central African Republic", LongName = "The Central African Republic"
               , CurrencyCode = "XAF", Languages = ["fr", "sg"]
               , UnofficialNames = [
                     "Central African Republic", "Zentralafrikanische Republik", "République Centrafricaine", "República Centroafricana", "中央アフリカ共和国"
                   , "Centraal-Afrikaanse Republiek", "中非"
                 ], Timezones = ["Africa/Bangui,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.中非))]
    CF = 140

   ,

    /// <summary>
    ///     喀麦隆
    /// </summary>
    [CountryInfo(CallingCode = 237,        Alpha3 = "CMR", ShortName = "Cameroon", LongName = "The Republic of Cameroon", CurrencyCode = "XAF"
               , Languages = ["en", "fr"], UnofficialNames = ["Cameroon", "Kamerun", "Cameroun", "Camerún", "カメルーン", "Kameroen", "喀麦隆"]
               , Timezones = ["Africa/Douala,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.喀麦隆))]
    CM = 120

   ,

    /// <summary>
    ///     佛得角
    /// </summary>
    [CountryInfo(CallingCode = 238,  Alpha3 = "CPV", ShortName = "Cabo Verde", LongName = "The Republic of Cabo Verde", CurrencyCode = "CVE"
               , Languages = ["pt"], UnofficialNames = ["Cape Verde", "Kap Verde", "Cap Vert", "Cabo Verde", "カーボベルデ", "Kaapverdië", "佛得角"]
               , Timezones = ["Atlantic/Cape_Verde,-0100"])]
    [ResourceDescription<Ln>(nameof(Ln.佛得角))]
    CV = 132

   ,

    /// <summary>
    ///     圣多美和普林西比
    /// </summary>
    [CountryInfo(CallingCode = 239, Alpha3 = "STP", ShortName = "Sao Tome and Principe", LongName = "The Democratic Republic of São Tomé and Príncipe"
               , CurrencyCode = "STD", Languages = ["pt"]
               , UnofficialNames = [
                     "São Tomé and Príncipe", "São Tomé und Príncipe", "São Tomé et Príncipe", "Santo Tomé y Príncipe", "サントメ・プリンシペ"
                   , "Sao Tomé en Principe", "圣多美和普林西比"
                 ], Timezones = ["Africa/Sao_Tome,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.圣多美和普林西比))]
    ST = 678

   ,

    /// <summary>
    ///     赤道几内亚
    /// </summary>
    [CountryInfo(CallingCode = 240,    Alpha3 = "GNQ", ShortName = "Equatorial Guinea", LongName = "The Republic of Equatorial Guinea"
               , CurrencyCode = "XAF", Languages = ["es", "fr"]
               , UnofficialNames = [
                     "Equatorial Guinea", "Äquatorial-Guinea", "Guinée Équatoriale", "Guinea Ecuatorial", "赤道ギニア", "Equatoriaal-Guinea", "赤道几内亚"
                 ], Timezones = ["Africa/Malabo,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.赤道几内亚))]
    GQ = 226

   ,

    /// <summary>
    ///     加蓬
    /// </summary>
    [CountryInfo(CallingCode = 241, Alpha3 = "GAB", ShortName = "Gabon", LongName = "The Gabonese Republic", CurrencyCode = "XAF", Languages = ["fr"]
               , UnofficialNames = ["Gabon", "Gabun", "Gabón", "ガボン", "加蓬"], Timezones = ["Africa/Libreville,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.加蓬))]
    GA = 266

   ,

    /// <summary>
    ///     刚果共和国
    /// </summary>
    [CountryInfo(CallingCode = 242,        Alpha3 = "COG", ShortName = "Congo", LongName = "The Republic of the Congo", CurrencyCode = "XAF"
               , Languages = ["fr", "ln"], UnofficialNames = ["Congo", "Kongo", "コンゴ共和国", "Congo [Republiek]", "Congo, Republic of", "刚果共和国"]
               , Timezones = ["Africa/Brazzaville,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.刚果共和国))]
    CG = 178

   ,

    /// <summary>
    ///     刚果民主共和国
    /// </summary>
    [CountryInfo(CallingCode = 243,                                 Alpha3 = "COD",       ShortName = "Congo (Democratic Republic of the)"
               , LongName = "The Democratic Republic of the Congo", CurrencyCode = "CDF", Languages = ["fr", "ln", "kg", "sw", "lu"]
               , UnofficialNames = [
                     "Congo (Dem. Rep.)", "Kongo (Dem. Rep.)", "Congo (Rep. Dem.)", "コンゴ民主共和国", "Congo [DRC]"
                   , "Congo (The Democratic Republic Of The)", "Democratic Republic of the Congo", "Congo, Democratic Republic of", "刚果民主共和国"
                 ], Timezones = ["Africa/Kinshasa,+0100", "Africa/Lubumbashi,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.刚果民主共和国))]
    CD = 180

   ,

    /// <summary>
    ///     安哥拉
    /// </summary>
    [CountryInfo(CallingCode = 244,  Alpha3 = "AGO", ShortName = "Angola", LongName = "The Republic of Angola", CurrencyCode = "AOA"
               , Languages = ["pt"], UnofficialNames = ["Angola", "アンゴラ", "安哥拉"], Timezones = ["Africa/Luanda,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.安哥拉))]
    AO = 024

   ,

    /// <summary>
    ///     几内亚比绍
    /// </summary>
    [CountryInfo(CallingCode = 245,  Alpha3 = "GNB", ShortName = "Guinea-Bissau", LongName = "The Republic of Guinea-Bissau", CurrencyCode = "XOF"
               , Languages = ["pt"], UnofficialNames = ["Guinea-Bissau", "Guinée-Bissau", "ギニアビサウ", "Guinee-Bissau", "Guinea Bissau", "几内亚比绍"]
               , Timezones = ["Africa/Bissau,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.几内亚比绍))]
    GW = 624

   ,

    /// <summary>
    ///     英属印度洋领地
    /// </summary>
    [CountryInfo(CallingCode = 246,    Alpha3 = "IOT", ShortName = "British Indian Ocean Territory", LongName = "The British Indian Ocean Territory"
               , CurrencyCode = "USD", Languages = ["en"]
               , UnofficialNames = [
                     "British Indian Ocean Territory", "Britisches Territorium im Indischen Ozean", "イギリス領インド洋地域"
                   , "Britse Gebieden in de Indische Oceaan", "英属印度洋领地"
                 ], Timezones = ["Indian/Chagos,+0600"])]
    [ResourceDescription<Ln>(nameof(Ln.英属印度洋领地))]
    IO = 086

   ,

    /// <summary>
    ///     塞舌尔
    /// </summary>
    [CountryInfo(CallingCode = 248,        Alpha3 = "SYC", ShortName = "Seychelles", LongName = "The Republic of Seychelles", CurrencyCode = "SCR"
               , Languages = ["fr", "en"], UnofficialNames = ["Seychelles", "Seychellen", "セーシェル", "塞舌尔"], Timezones = ["Indian/Mahe,+0400"])]
    [ResourceDescription<Ln>(nameof(Ln.塞舌尔))]
    SC = 690

   ,

    /// <summary>
    ///     苏丹
    /// </summary>
    [CountryInfo(CallingCode = 249,        Alpha3 = "SDN", ShortName = "Sudan", LongName = "The Republic of the Sudan", CurrencyCode = "SDG"
               , Languages = ["ar", "en"], UnofficialNames = ["Sudan", "السودان", "Soudan", "Sudán", "スーダン", "Soedan", "苏丹"]
               , Timezones = ["Africa/Khartoum,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.苏丹))]
    SD = 729

   ,

    /// <summary>
    ///     卢旺达
    /// </summary>
    [CountryInfo(CallingCode = 250,              Alpha3 = "RWA", ShortName = "Rwanda", LongName = "The Republic of Rwanda", CurrencyCode = "RWF"
               , Languages = ["rw", "en", "fr"], UnofficialNames = ["Rwanda", "Ruanda", "ルワンダ", "卢旺达"], Timezones = ["Africa/Kigali,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.卢旺达))]
    RW = 646

   ,

    /// <summary>
    ///     埃塞俄比亚
    /// </summary>
    [CountryInfo(CallingCode = 251,    Alpha3 = "ETH", ShortName = "Ethiopia", LongName = "The Federal Democratic Republic of Ethiopia"
               , CurrencyCode = "ETB", Languages = ["am"]
               , UnofficialNames = ["Ethiopia", "Äthiopien", "Éthiopie", "Etiopía", "エチオピア", "Ethiopië", "埃塞俄比亚"]
               , Timezones = ["Africa/Addis_Ababa,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.埃塞俄比亚))]
    ET = 231

   ,

    /// <summary>
    ///     索马里
    /// </summary>
    [CountryInfo(CallingCode = 252,        Alpha3 = "SOM", ShortName = "Somalia", LongName = "The Federal Republic of Somalia", CurrencyCode = "SOS"
               , Languages = ["so", "ar"], UnofficialNames = ["Somalia", "الصومال", "ソマリア", "Somalië", "索马里"]
               , Timezones = ["Africa/Mogadishu,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.索马里))]
    SO = 706

   ,

    /// <summary>
    ///     吉布提
    /// </summary>
    [CountryInfo(CallingCode = 253,        Alpha3 = "DJI", ShortName = "Djibouti", LongName = "The Republic of Djibouti", CurrencyCode = "DJF"
               , Languages = ["ar", "fr"], UnofficialNames = ["Djibouti", "جيبوتي", "Dschibuti", "ジブチ", "吉布提"]
               , Timezones = ["Africa/Djibouti,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.吉布提))]
    DJ = 262

   ,

    /// <summary>
    ///     肯尼亚
    /// </summary>
    [CountryInfo(CallingCode = 254,        Alpha3 = "KEN", ShortName = "Kenya", LongName = "The Republic of Kenya", CurrencyCode = "KES"
               , Languages = ["en", "sw"], UnofficialNames = ["Kenya", "Kenia", "ケニア", "肯尼亚"], Timezones = ["Africa/Nairobi,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.肯尼亚))]
    KE = 404

   ,

    /// <summary>
    ///     坦桑尼亚
    /// </summary>
    [CountryInfo(CallingCode = 255,    Alpha3 = "TZA", ShortName = "Tanzania, United Republic of", LongName = "The United Republic of Tanzania"
               , CurrencyCode = "TZS", Languages = ["sw", "en"]
               , UnofficialNames = ["Tanzania", "Tansania", "Tanzanie", "タンザニア", "Tanzania United Republic", "坦桑尼亚"]
               , Timezones = ["Africa/Dar_es_Salaam,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.坦桑尼亚))]
    TZ = 834

   ,

    /// <summary>
    ///     乌干达
    /// </summary>
    [CountryInfo(CallingCode = 256,        Alpha3 = "UGA", ShortName = "Uganda", LongName = "The Republic of Uganda", CurrencyCode = "UGX"
               , Languages = ["en", "sw"], UnofficialNames = ["Uganda", "ウガンダ", "Oeganda", "乌干达"], Timezones = ["Africa/Kampala,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.乌干达))]
    UG = 800

   ,

    /// <summary>
    ///     布隆迪
    /// </summary>
    [CountryInfo(CallingCode = 257,        Alpha3 = "BDI", ShortName = "Burundi", LongName = "The Republic of Burundi", CurrencyCode = "BIF"
               , Languages = ["fr", "rn"], UnofficialNames = ["Burundi", "ブルンジ", "布隆迪"], Timezones = ["Africa/Bujumbura,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.布隆迪))]
    BI = 108

   ,

    /// <summary>
    ///     莫桑比克
    /// </summary>
    [CountryInfo(CallingCode = 258,  Alpha3 = "MOZ", ShortName = "Mozambique", LongName = "The Republic of Mozambique", CurrencyCode = "MZN"
               , Languages = ["pt"], UnofficialNames = ["Mozambique", "Mosambik", "モザンビーク", "莫桑比克"], Timezones = ["Africa/Maputo,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.莫桑比克))]
    MZ = 508

   ,

    /// <summary>
    ///     赞比亚
    /// </summary>
    [CountryInfo(CallingCode = 260,  Alpha3 = "ZMB", ShortName = "Zambia", LongName = "The Republic of Zambia", CurrencyCode = "ZMW"
               , Languages = ["en"], UnofficialNames = ["Zambia", "Sambia", "Zambie", "ザンビア", "赞比亚"], Timezones = ["Africa/Lusaka,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.赞比亚))]
    ZM = 894

   ,

    /// <summary>
    ///     马达加斯加
    /// </summary>
    [CountryInfo(CallingCode = 261,        Alpha3 = "MDG", ShortName = "Madagascar", LongName = "The Republic of Madagascar", CurrencyCode = "MGA"
               , Languages = ["fr", "mg"], UnofficialNames = ["Madagascar", "Madagaskar", "the Republic of Madagascar", "マダガスカル", "马达加斯加"]
               , Timezones = ["Indian/Antananarivo,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.马达加斯加))]
    MG = 450

   ,

    /// <summary>
    ///     留尼汪
    /// </summary>
    [CountryInfo(CallingCode = 262, Alpha3 = "REU", ShortName = "Réunion", LongName = "Réunion", CurrencyCode = "EUR", Languages = ["fr"]
               , UnofficialNames = ["Réunion", "Reunión", "Reunion", "レユニオン", "留尼汪"], IsPreferred = true, Timezones = ["Indian/Reunion,+0400"])]
    [ResourceDescription<Ln>(nameof(Ln.留尼汪))]
    RE = 638

   ,

    /// <summary>
    ///     法属南部和南极领地
    /// </summary>
    [CountryInfo(CallingCode = 262,    Alpha3 = "ATF", ShortName = "French Southern Territories", LongName = "The French Southern and Antarctic Lands"
               , CurrencyCode = "EUR", Languages = ["fr"]
               , UnofficialNames = [
                     "French Southern Territories", "Französische Süd- und Antarktisgebiete", "Terres Australes Françaises"
                   , "Territorios Franceses del Sur", "フランス領南方・南極地域", "Franse Gebieden in de zuidelijke Indische Oceaan"
                   , "French Southern and Antarctic Lands", "法属南部和南极领地"
                 ], Timezones = ["Indian/Kerguelen,+0500"])]
    [ResourceDescription<Ln>(nameof(Ln.法属南部和南极领地))]
    TF = 260

   ,

    /// <summary>
    ///     马约特
    /// </summary>
    [CountryInfo(CallingCode = 262,    CallingSubCode = ["269", "639"], Alpha3 = "MYT", ShortName = "Mayotte", LongName = "The Department of Mayotte"
               , CurrencyCode = "EUR", Languages = ["fr"], UnofficialNames = ["Mayotte", "マヨット", "马约特"], Timezones = ["Indian/Mayotte,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.马约特))]
    YT = 175

   ,

    /// <summary>
    ///     津巴布韦
    /// </summary>
    [CountryInfo(CallingCode = 263,              Alpha3 = "ZWE", ShortName = "Zimbabwe", LongName = "The Republic of Zimbabwe", CurrencyCode = "USD"
               , Languages = ["en", "sn", "nd"], UnofficialNames = ["Zimbabwe", "Simbabwe", "Zimbabue", "ジンバブエ", "津巴布韦"]
               , Timezones = ["Africa/Harare,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.津巴布韦))]
    ZW = 716

   ,

    /// <summary>
    ///     纳米比亚
    /// </summary>
    [CountryInfo(CallingCode = 264,        Alpha3 = "NAM", ShortName = "Namibia", LongName = "The Republic of Namibia", CurrencyCode = "NAD"
               , Languages = ["en", "af"], UnofficialNames = ["Namibia", "Namibie", "ナミビア", "Namibië", "纳米比亚"]
               , Timezones = ["Africa/Windhoek,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.纳米比亚))]
    NA = 516

   ,

    /// <summary>
    ///     马拉维
    /// </summary>
    [CountryInfo(CallingCode = 265,        Alpha3 = "MWI", ShortName = "Malawi", LongName = "The Republic of Malawi", CurrencyCode = "MWK"
               , Languages = ["en", "ny"], UnofficialNames = ["Malawi", "マラウイ", "马拉维"], Timezones = ["Africa/Blantyre,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.马拉维))]
    MW = 454

   ,

    /// <summary>
    ///     莱索托
    /// </summary>
    [CountryInfo(CallingCode = 266,        Alpha3 = "LSO", ShortName = "Lesotho", LongName = "The Kingdom of Lesotho", CurrencyCode = "LSL"
               , Languages = ["en", "st"], UnofficialNames = ["Lesotho", "レソト", "莱索托"], Timezones = ["Africa/Maseru,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.莱索托))]
    LS = 426

   ,

    /// <summary>
    ///     博茨瓦纳
    /// </summary>
    [CountryInfo(CallingCode = 267,        Alpha3 = "BWA", ShortName = "Botswana", LongName = "The Republic of Botswana", CurrencyCode = "BWP"
               , Languages = ["en", "tn"], UnofficialNames = ["Botswana", "ボツワナ", "博茨瓦纳"], Timezones = ["Africa/Gaborone,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.博茨瓦纳))]
    BW = 072

   ,

    /// <summary>
    ///     斯威士兰
    /// </summary>
    [CountryInfo(CallingCode = 268,        Alpha3 = "SWZ", ShortName = "Eswatini", LongName = "The Kingdom of Eswatini", CurrencyCode = "SZL"
               , Languages = ["en", "ss"], UnofficialNames = ["Swaziland", "Swasiland", "Suazilandia", "スワジランド", "斯威士兰"]
               , Timezones = ["Africa/Mbabane,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.斯威士兰))]
    SZ = 748

   ,

    /// <summary>
    ///     科摩罗
    /// </summary>
    [CountryInfo(CallingCode = 269,        Alpha3 = "COM", ShortName = "Comoros", LongName = "The Union of the Comoros", CurrencyCode = "KMF"
               , Languages = ["ar", "fr"], UnofficialNames = ["Comoros", "Union der Komoren", "Comores", "コモロ", "Comoren", "科摩罗"]
               , Timezones = ["Indian/Comoro,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.科摩罗))]
    KM = 174

   ,

    /// <summary>
    ///     圣赫勒拿
    /// </summary>
    [CountryInfo(CallingCode = 290,                                         Alpha3 = "SHN", ShortName = "Saint Helena, Ascension and Tristan da Cunha"
               , LongName = "Saint Helena, Ascension and Tristan da Cunha", CurrencyCode = "SHP", Languages = ["en"]
               , UnofficialNames = [
                     "Saint Helena", "Sankt Helena", "Sainte Hélène", "Santa Helena", "セントヘレナ・アセンションおよびトリスタンダクーニャ", "Sint-Helena"
                   , "Saint Helena, Ascension and Tristan da Cunha", "圣赫勒拿"
                 ], Timezones = ["Atlantic/St_Helena,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.圣赫勒拿))]
    SH = 654

   ,

    /// <summary>
    ///     厄立特里亚
    /// </summary>
    [CountryInfo(CallingCode = 291,              Alpha3 = "ERI", ShortName = "Eritrea", LongName = "The State of Eritrea", CurrencyCode = "ETB"
               , Languages = ["en", "ar", "ti"], UnofficialNames = ["Eritrea", "إريتريا", "Érythrée", "エリトリア", "厄立特里亚"]
               , Timezones = ["Africa/Asmara,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.厄立特里亚))]
    ER = 232

   ,

    /// <summary>
    ///     阿鲁巴
    /// </summary>
    [CountryInfo(CallingCode = 297, Alpha3 = "ABW", ShortName = "Aruba", LongName = "Aruba", CurrencyCode = "AWG", Languages = ["nl"]
               , UnofficialNames = ["Aruba", "アルバ", "阿鲁巴"], Timezones = ["America/Aruba,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.阿鲁巴))]
    AW = 533

   ,

    /// <summary>
    ///     法罗群岛
    /// </summary>
    [CountryInfo(CallingCode = 298,  Alpha3 = "FRO", ShortName = "Faroe Islands", LongName = "The Faroe Islands", CurrencyCode = "DKK"
               , Languages = ["fo"], UnofficialNames = ["Faroe Islands", "Färöer-Inseln", "Îles Féroé", "Islas Faroe", "フェロー諸島", "Faeröer", "法罗群岛"]
               , Timezones = ["Atlantic/Faroe,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.法罗群岛))]
    FO = 234

   ,

    /// <summary>
    ///     格陵兰
    /// </summary>
    [CountryInfo(CallingCode = 299, Alpha3 = "GRL", ShortName = "Greenland", LongName = "Kalaallit Nunaat", CurrencyCode = "DKK", Languages = ["kl"]
               , UnofficialNames = ["Greenland", "Grönland", "Groenland", "Groenlandia", "グリーンランド", "格陵兰"]
               , Timezones = ["America/Danmarkshavn,+0000", "America/Nuuk,-0200", "America/Scoresbysund,-0200", "America/Thule,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.格陵兰))]
    GL = 304

   ,

    /// <summary>
    ///     直布罗陀
    /// </summary>
    [CountryInfo(CallingCode = 350, Alpha3 = "GIB", ShortName = "Gibraltar", LongName = "Gibraltar", CurrencyCode = "GIP", Languages = ["en"]
               , UnofficialNames = ["Gibraltar", "ジブラルタル", "直布罗陀"], Timezones = ["Europe/Gibraltar,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.直布罗陀))]
    GI = 292

   ,

    /// <summary>
    ///     葡萄牙
    /// </summary>
    [CountryInfo(CallingCode = 351,  Alpha3 = "PRT", ShortName = "Portugal", LongName = "The Portuguese Republic", CurrencyCode = "EUR"
               , Languages = ["pt"], UnofficialNames = ["Portugal", "ポルトガル", "葡萄牙"]
               , Timezones = ["Atlantic/Azores,-0100", "Atlantic/Madeira,+0000", "Europe/Lisbon,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.葡萄牙))]
    PT = 620

   ,

    /// <summary>
    ///     卢森堡
    /// </summary>
    [CountryInfo(CallingCode = 352, Alpha3 = "LUX", ShortName = "Luxembourg", LongName = "The Grand Duchy of Luxembourg", CurrencyCode = "EUR"
               , Languages = ["fr", "de", "lb"], UnofficialNames = ["Luxembourg", "Luxemburg", "Luxemburgo", "ルクセンブルク", "卢森堡"]
               , Timezones = ["Europe/Luxembourg,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.卢森堡))]
    LU = 442

   ,

    /// <summary>
    ///     爱尔兰
    /// </summary>
    [CountryInfo(CallingCode = 353, Alpha3 = "IRL", ShortName = "Ireland", LongName = "Ireland", CurrencyCode = "EUR", Languages = ["en", "ga"]
               , UnofficialNames = ["Ireland", "Irland", "Irlande", "Irlanda", "アイルランド", "Ierland", "爱尔兰"], Timezones = ["Europe/Dublin,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.爱尔兰))]
    IE = 372

   ,

    /// <summary>
    ///     冰岛
    /// </summary>
    [CountryInfo(CallingCode = 354, Alpha3 = "ISL", ShortName = "Iceland", LongName = "Iceland", CurrencyCode = "ISK", Languages = ["is"]
               , UnofficialNames = ["Iceland", "Island", "Islande", "Islandia", "アイスランド", "IJsland", "冰岛"], Timezones = ["Atlantic/Reykjavik,+0000"])]
    [ResourceDescription<Ln>(nameof(Ln.冰岛))]
    IS = 352

   ,

    /// <summary>
    ///     阿尔巴尼亚
    /// </summary>
    [CountryInfo(CallingCode = 355,  Alpha3 = "ALB", ShortName = "Albania", LongName = "The Republic of Albania", CurrencyCode = "ALL"
               , Languages = ["sq"], UnofficialNames = ["Albania", "Albanien", "Albanie", "アルバニア", "Albanië", "阿尔巴尼亚"]
               , Timezones = ["Europe/Tirane,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.阿尔巴尼亚))]
    AL = 008

   ,

    /// <summary>
    ///     马耳他
    /// </summary>
    [CountryInfo(CallingCode = 356,        Alpha3 = "MLT", ShortName = "Malta", LongName = "The Republic of Malta", CurrencyCode = "EUR"
               , Languages = ["mt", "en"], UnofficialNames = ["Malta", "Malte", "マルタ", "马耳他"], Timezones = ["Europe/Malta,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.马耳他))]
    MT = 470

   ,

    /// <summary>
    ///     塞浦路斯
    /// </summary>
    [CountryInfo(CallingCode = 357,              Alpha3 = "CYP", ShortName = "Cyprus", LongName = "The Republic of Cyprus", CurrencyCode = "EUR"
               , Languages = ["el", "tr", "hy"], UnofficialNames = ["Cyprus", "Zypern", "Chypre", "Chipre", "キプロス", "塞浦路斯"]
               , Timezones = ["Asia/Famagusta,+0200", "Asia/Nicosia,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.塞浦路斯))]
    CY = 196

   ,

    /// <summary>
    ///     奥兰
    /// </summary>
    [CountryInfo(CallingCode = 358,  CallingSubCode = ["18"], Alpha3 = "ALA", ShortName = "Åland Islands", LongName = "Åland", CurrencyCode = "EUR"
               , Languages = ["sv"], UnofficialNames = ["Åland Islands", "Åland", "オーランド諸島", "Ålandeilanden", "奥兰"]
               , Timezones = ["Europe/Mariehamn,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.奥兰))]
    AX = 248

   ,

    /// <summary>
    ///     芬兰
    /// </summary>
    [CountryInfo(CallingCode = 358,        Alpha3 = "FIN", ShortName = "Finland", LongName = "The Republic of Finland", CurrencyCode = "EUR"
               , Languages = ["fi", "sv"], UnofficialNames = ["Finland", "Finnland", "Finlande", "Finlandia", "フィンランド", "芬兰"], IsPreferred = true
               , Timezones = ["Europe/Helsinki,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.芬兰))]
    FI = 246

   ,

    /// <summary>
    ///     保加利亚
    /// </summary>
    [CountryInfo(CallingCode = 359,  Alpha3 = "BGR", ShortName = "Bulgaria", LongName = "The Republic of Bulgaria", CurrencyCode = "BGN"
               , Languages = ["bg"], UnofficialNames = ["Bulgaria", "България", "Bulgarien", "Bulgarie", "ブルガリア", "Bulgarije", "保加利亚"]
               , Timezones = ["Europe/Sofia,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.保加利亚))]
    BG = 100

   ,

    /// <summary>
    ///     立陶宛
    /// </summary>
    [CountryInfo(CallingCode = 370, Alpha3 = "LTU", ShortName = "Lithuania", LongName = "The Republic of Lithuania", CurrencyCode = "EUR"
               , Languages = ["lt"]
               , UnofficialNames = ["Lithuania", "Litauen", "Lituanie", "Lituania", "リトアニア", "Litouwen", "Літва", "Lietuva", "立陶宛"]
               , Timezones = ["Europe/Vilnius,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.立陶宛))]
    LT = 440

   ,

    /// <summary>
    ///     拉脱维亚
    /// </summary>
    [CountryInfo(CallingCode = 371,  Alpha3 = "LVA", ShortName = "Latvia", LongName = "The Republic of Latvia", CurrencyCode = "EUR"
               , Languages = ["lv"], UnofficialNames = ["Latvia", "Lettland", "Lettonie", "Letonia", "ラトビア", "Letland", "拉脱维亚"]
               , Timezones = ["Europe/Riga,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.拉脱维亚))]
    LV = 428

   ,

    /// <summary>
    ///     爱沙尼亚
    /// </summary>
    [CountryInfo(CallingCode = 372,  Alpha3 = "EST", ShortName = "Estonia", LongName = "The Republic of Estonia", CurrencyCode = "EUR"
               , Languages = ["et"], UnofficialNames = ["Estonia", "Estland", "Estonie", "エストニア", "爱沙尼亚"], Timezones = ["Europe/Tallinn,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.爱沙尼亚))]
    EE = 233

   ,

    /// <summary>
    ///     摩尔多瓦
    /// </summary>
    [CountryInfo(CallingCode = 373, Alpha3 = "MDA", ShortName = "Moldova (Republic of)", LongName = "The Republic of Moldova", CurrencyCode = "MDL"
               , Languages = ["ro"]
               , UnofficialNames = ["Moldova", "Moldawien", "Moldavie", "Moldavia", "the Republic of Moldova", "モルドバ共和国", "Moldavië", "摩尔多瓦"]
               , Timezones = ["Europe/Chisinau,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.摩尔多瓦))]
    MD = 498

   ,

    /// <summary>
    ///     亚美尼亚
    /// </summary>
    [CountryInfo(CallingCode = 374,        Alpha3 = "ARM", ShortName = "Armenia", LongName = "The Republic of Armenia", CurrencyCode = "AMD"
               , Languages = ["hy", "ru"], UnofficialNames = ["Armenia", "Armenien", "Arménie", "アルメニア", "Armenië", "亚美尼亚"]
               , Timezones = ["Asia/Yerevan,+0400"])]
    [ResourceDescription<Ln>(nameof(Ln.亚美尼亚))]
    AM = 051

   ,

    /// <summary>
    ///     白俄罗斯
    /// </summary>
    [CountryInfo(CallingCode = 375, Alpha3 = "BLR", ShortName = "Belarus", LongName = "The Republic of Belarus", CurrencyCode = "BYN"
               , Languages = ["be", "ru"]
               , UnofficialNames = ["Belarus", "Weißrussland", "Biélorussie", "Bielorrusia", "ベラルーシ", "Wit-Rusland", "Беларусь", "白俄罗斯"]
               , Timezones = ["Europe/Minsk,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.白俄罗斯))]
    BY = 112

   ,

    /// <summary>
    ///     安道尔
    /// </summary>
    [CountryInfo(CallingCode = 376,  Alpha3 = "AND", ShortName = "Andorra", LongName = "The Principality of Andorra", CurrencyCode = "EUR"
               , Languages = ["ca"], UnofficialNames = ["Andorre", "Andorra", "アンドラ", "安道尔"], Timezones = ["Europe/Andorra,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.安道尔))]
    AD = 020

   ,

    /// <summary>
    ///     摩纳哥
    /// </summary>
    [CountryInfo(CallingCode = 377,  Alpha3 = "MCO", ShortName = "Monaco", LongName = "The Principality of Monaco", CurrencyCode = "EUR"
               , Languages = ["fr"], UnofficialNames = ["Monaco", "Mónaco", "モナコ", "摩纳哥"], Timezones = ["Europe/Monaco,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.摩纳哥))]
    MC = 492

   ,

    /// <summary>
    ///     圣马力诺
    /// </summary>
    [CountryInfo(CallingCode = 378,  Alpha3 = "SMR", ShortName = "San Marino", LongName = "The Republic of San Marino", CurrencyCode = "EUR"
               , Languages = ["it"], UnofficialNames = ["San Marino", "Saint-Marin", "サンマリノ", "圣马力诺"], Timezones = ["Europe/San_Marino,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.圣马力诺))]
    SM = 674

   ,

    /// <summary>
    ///     梵蒂冈
    /// </summary>
    [CountryInfo(CallingCode = 39, CallingSubCode = ["06698"], Alpha3 = "VAT", ShortName = "Holy See", LongName = "The Holy See", CurrencyCode = "EUR"
               , Languages = ["it", "la"]
               , UnofficialNames = [
                     "Vatican City", "Vatikan", "Cité du Vatican", "Ciudad del Vaticano", "バチカン市国", "Vaticaanstad", "Vatican City State (Holy See)"
                   , "梵蒂冈"
                 ], Timezones = ["Europe/Vatican,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.梵蒂冈))]
    VA = 336

   ,

    /// <summary>
    ///     乌克兰
    /// </summary>
    [CountryInfo(CallingCode = 380, Alpha3 = "UKR", ShortName = "Ukraine", LongName = "Ukraine", CurrencyCode = "UAH", Languages = ["uk"]
               , UnofficialNames = ["Ukraine", "Ucrania", "ウクライナ", "Oekraïne", "Украина", "Україна", "Украіна", "乌克兰"]
               , Timezones = ["Europe/Kyiv,+0200", "Europe/Simferopol,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.乌克兰))]
    UA = 804

   ,

    /// <summary>
    ///     塞尔维亚
    /// </summary>
    [CountryInfo(CallingCode = 381,  Alpha3 = "SRB", ShortName = "Serbia", LongName = "The Republic of Serbia", CurrencyCode = "RSD"
               , Languages = ["sr"], UnofficialNames = ["Serbia", "Serbien", "Serbie", "セルビア", "Servië", "塞尔维亚"]
               , Timezones = ["Europe/Belgrade,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.塞尔维亚))]
    RS = 688

   ,

    /// <summary>
    ///     黑山
    /// </summary>
    [CountryInfo(CallingCode = 382,                    Alpha3 = "MNE", ShortName = "Montenegro", LongName = "Montenegro", CurrencyCode = "EUR"
               , Languages = ["sr", "bs", "sq", "hr"], UnofficialNames = ["Crna Gora", "Montenegro", "モンテネグロ", "黑山"]
               , Timezones = ["Europe/Podgorica,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.黑山))]
    ME = 499

   ,

    /// <summary>
    ///     克罗地亚
    /// </summary>
    [CountryInfo(CallingCode = 385,  Alpha3 = "HRV", ShortName = "Croatia", LongName = "The Republic of Croatia", CurrencyCode = "EUR"
               , Languages = ["hr"], UnofficialNames = ["Croatia", "Kroatien", "Croatie", "Croacia", "クロアチア", "Kroatië", "Croatia (Hrvatska)", "克罗地亚"]
               , Timezones = ["Europe/Zagreb,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.克罗地亚))]
    HR = 191

   ,

    /// <summary>
    ///     斯洛文尼亚
    /// </summary>
    [CountryInfo(CallingCode = 386,  Alpha3 = "SVN", ShortName = "Slovenia", LongName = "The Republic of Slovenia", CurrencyCode = "EUR"
               , Languages = ["sl"], UnofficialNames = ["Slovenia", "Slowenien", "Slovénie", "Eslovenia", "スロベニア", "Slovenië", "斯洛文尼亚"]
               , Timezones = ["Europe/Ljubljana,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.斯洛文尼亚))]
    SI = 705

   ,

    /// <summary>
    ///     波黑
    /// </summary>
    [CountryInfo(CallingCode = 387, Alpha3 = "BIH", ShortName = "Bosnia and Herzegovina", LongName = "Bosnia and Herzegovina", CurrencyCode = "BAM"
               , Languages = ["bs", "hr", "sr"]
               , UnofficialNames = [
                     "Bosnia and Herzegovina", "Bosnien und Herzegowina", "Bosnie et Herzégovine", "Bosnia y Herzegovina", "ボスニア・ヘルツェゴビナ"
                   , "Bosnië en Herzegovina", "Bosnia Herzegovina", "波黑"
                 ], Timezones = ["Europe/Sarajevo,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.波黑))]
    BA = 070

   ,

    /// <summary>
    ///     北马其顿
    /// </summary>
    [CountryInfo(CallingCode = 389, Alpha3 = "MKD", ShortName = "North Macedonia", LongName = "The Republic of North Macedonia", CurrencyCode = "MKD"
               , Languages = ["mk"]
               , UnofficialNames = [
                     "Macedonia", "Mazedonien", "Macédoine", "F.Y.R.O.M (Macedonia)", "マケドニア旧ユーゴスラビア共和国", "Macedonië [FYROM]"
                   , "Macedonia (The Former Yugoslav Republic of)", "North Macedonia", "Macedonia (FYROM)", "北马其顿"
                 ], Timezones = ["Europe/Skopje,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.北马其顿))]
    MK = 807

   ,

    /// <summary>
    ///     捷克
    /// </summary>
    [CountryInfo(CallingCode = 420, Alpha3 = "CZE", ShortName = "Czechia", LongName = "The Czech Republic", CurrencyCode = "CZK", Languages = ["cs"]
               , UnofficialNames = [
                     "Czech Republic", "Tschechische Republik", "République Tchèque", "República Checa", "チェコ", "Tsjechië", "Czechia"
                   , "Česká republika", "捷克"
                 ], Timezones = ["Europe/Prague,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.捷克))]
    CZ = 203

   ,

    /// <summary>
    ///     斯洛伐克
    /// </summary>
    [CountryInfo(CallingCode = 421, Alpha3 = "SVK", ShortName = "Slovakia", LongName = "The Slovak Republic", CurrencyCode = "EUR", Languages = ["sk"]
               , UnofficialNames = ["Slovakia", "Slowakei", "Slovaquie", "República Eslovaca", "スロバキア", "Slowakije", "斯洛伐克"]
               , Timezones = ["Europe/Bratislava,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.斯洛伐克))]
    SK = 703

   ,

    /// <summary>
    ///     列支敦士登
    /// </summary>
    [CountryInfo(CallingCode = 423,  Alpha3 = "LIE", ShortName = "Liechtenstein", LongName = "The Principality of Liechtenstein", CurrencyCode = "CHF"
               , Languages = ["de"], UnofficialNames = ["Liechtenstein", "リヒテンシュタイン", "列支敦士登"], Timezones = ["Europe/Vaduz,+0100"])]
    [ResourceDescription<Ln>(nameof(Ln.列支敦士登))]
    LI = 438

   ,

    /// <summary>
    ///     福克兰群岛
    /// </summary>
    [CountryInfo(CallingCode = 500, Alpha3 = "FLK", ShortName = "Falkland Islands (Malvinas)", LongName = "The Falkland Islands", CurrencyCode = "FKP"
               , Languages = ["en"]
               , UnofficialNames = [
                     "Falkland Islands", "Falklandinseln", "Îles Malouines", "Islas Malvinas", "フォークランド（マルビナス）諸島", "Falklandeilanden [Islas Malvinas]"
                   , "福克兰群岛"
                 ], IsPreferred = true, Timezones = ["Atlantic/Stanley,-0300"])]
    [ResourceDescription<Ln>(nameof(Ln.福克兰群岛))]
    FK = 238

   ,

    /// <summary>
    ///     南乔治亚和南桑威奇群岛
    /// </summary>
    [CountryInfo(CallingCode = 500,                                         Alpha3 = "SGS", ShortName = "South Georgia and the South Sandwich Islands"
               , LongName = "South Georgia and the South Sandwich Islands", CurrencyCode = "GBP", Languages = ["en"]
               , UnofficialNames = [
                     "South Georgia", "South Georgia and the South Sandwich Islands", "Südgeorgien und die Südlichen Sandwichinseln"
                   , "サウスジョージア・サウスサンドウィッチ諸島", "Zuid-Georgia en Zuidelijke Sandwicheilanden", "南乔治亚和南桑威奇群岛"
                 ], Timezones = ["Atlantic/South_Georgia,-0200"])]
    [ResourceDescription<Ln>(nameof(Ln.南乔治亚和南桑威奇群岛))]
    GS = 239

   ,

    /// <summary>
    ///     伯利兹
    /// </summary>
    [CountryInfo(CallingCode = 501, Alpha3 = "BLZ", ShortName = "Belize", LongName = "Belize", CurrencyCode = "BZD", Languages = ["en", "es"]
               , UnofficialNames = ["Belize", "Belice", "ベリーズ", "伯利兹"], Timezones = ["America/Belize,-0600"])]
    [ResourceDescription<Ln>(nameof(Ln.伯利兹))]
    BZ = 084

   ,

    /// <summary>
    ///     危地马拉
    /// </summary>
    [CountryInfo(CallingCode = 502,  Alpha3 = "GTM", ShortName = "Guatemala", LongName = "The Republic of Guatemala", CurrencyCode = "GTQ"
               , Languages = ["es"], UnofficialNames = ["Guatemala", "グアテマラ", "危地马拉"], Timezones = ["America/Guatemala,-0600"])]
    [ResourceDescription<Ln>(nameof(Ln.危地马拉))]
    GT = 320

   ,

    /// <summary>
    ///     萨尔瓦多
    /// </summary>
    [CountryInfo(CallingCode = 503,  Alpha3 = "SLV", ShortName = "El Salvador", LongName = "The Republic of El Salvador", CurrencyCode = "USD"
               , Languages = ["es"], UnofficialNames = ["El Salvador", "Salvador", "エルサルバドル", "萨尔瓦多"], Timezones = ["America/El_Salvador,-0600"])]
    [ResourceDescription<Ln>(nameof(Ln.萨尔瓦多))]
    SV = 222

   ,

    /// <summary>
    ///     洪都拉斯
    /// </summary>
    [CountryInfo(CallingCode = 504,  Alpha3 = "HND", ShortName = "Honduras", LongName = "The Republic of Honduras", CurrencyCode = "HNL"
               , Languages = ["es"], UnofficialNames = ["Honduras", "ホンジュラス", "洪都拉斯"], Timezones = ["America/Tegucigalpa,-0600"])]
    [ResourceDescription<Ln>(nameof(Ln.洪都拉斯))]
    HN = 340

   ,

    /// <summary>
    ///     尼加拉瓜
    /// </summary>
    [CountryInfo(CallingCode = 505,  Alpha3 = "NIC", ShortName = "Nicaragua", LongName = "The Republic of Nicaragua", CurrencyCode = "NIO"
               , Languages = ["es"], UnofficialNames = ["Nicaragua", "ニカラグア", "尼加拉瓜"], Timezones = ["America/Managua,-0600"])]
    [ResourceDescription<Ln>(nameof(Ln.尼加拉瓜))]
    NI = 558

   ,

    /// <summary>
    ///     哥斯达黎加
    /// </summary>
    [CountryInfo(CallingCode = 506,  Alpha3 = "CRI", ShortName = "Costa Rica", LongName = "The Republic of Costa Rica", CurrencyCode = "CRC"
               , Languages = ["es"], UnofficialNames = ["Costa Rica", "コスタリカ", "哥斯达黎加"], Timezones = ["America/Costa_Rica,-0600"])]
    [ResourceDescription<Ln>(nameof(Ln.哥斯达黎加))]
    CR = 188

   ,

    /// <summary>
    ///     巴拿马
    /// </summary>
    [CountryInfo(CallingCode = 507,  Alpha3 = "PAN", ShortName = "Panama", LongName = "The Republic of Panamá", CurrencyCode = "PAB"
               , Languages = ["es"], UnofficialNames = ["Panama", "Panamá", "パナマ", "巴拿马"], Timezones = ["America/Panama,-0500"])]
    [ResourceDescription<Ln>(nameof(Ln.巴拿马))]
    PA = 591

   ,

    /// <summary>
    ///     圣皮埃尔和密克隆
    /// </summary>
    [CountryInfo(CallingCode = 508,                                                   Alpha3 = "SPM",       ShortName = "Saint Pierre and Miquelon"
               , LongName = "The Overseas Collectivity of Saint-Pierre and Miquelon", CurrencyCode = "EUR", Languages = ["fr"]
               , UnofficialNames = [
                     "Saint Pierre and Miquelon", "Saint-Pierre und Miquelon", "Saint-Pierre-et-Miquelon", "San Pedro y Miquelón", "サンピエール島・ミクロン島"
                   , "Saint Pierre en Miquelon", "圣皮埃尔和密克隆"
                 ], Timezones = ["America/Miquelon,-0300"])]
    [ResourceDescription<Ln>(nameof(Ln.圣皮埃尔和密克隆))]
    PM = 666

   ,

    /// <summary>
    ///     海地
    /// </summary>
    [CountryInfo(CallingCode = 509,        Alpha3 = "HTI", ShortName = "Haiti", LongName = "The Republic of Haiti", CurrencyCode = "HTG"
               , Languages = ["fr", "ht"], UnofficialNames = ["Haiti", "ハイチ", "Haïti", "海地"], Timezones = ["America/Port-au-Prince,-0500"])]
    [ResourceDescription<Ln>(nameof(Ln.海地))]
    HT = 332

   ,

    /// <summary>
    ///     圣巴泰勒米
    /// </summary>
    [CountryInfo(CallingCode = 590,    Alpha3 = "BLM",     ShortName = "Saint Barthélemy", LongName = "The Collectivity of Saint-Barthélemy"
               , CurrencyCode = "EUR", Languages = ["fr"], UnofficialNames = ["Saint Barthélemy", "Saint-Barthélemy", "サン・バルテルミー", "圣巴泰勒米"]
               , Timezones = ["America/St_Barthelemy,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.圣巴泰勒米))]
    BL = 652

   ,

    /// <summary>
    ///     瓜德罗普
    /// </summary>
    [CountryInfo(CallingCode = 590, Alpha3 = "GLP", ShortName = "Guadeloupe", LongName = "Guadeloupe", CurrencyCode = "EUR", Languages = ["fr"]
               , UnofficialNames = ["Guadeloupe", "Guadalupe", "グアドループ", "瓜德罗普"], IsPreferred = true, Timezones = ["America/Guadeloupe,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.瓜德罗普))]
    GP = 312

   ,

    /// <summary>
    ///     法属圣马丁
    /// </summary>
    [CountryInfo(CallingCode = 590,    Alpha3 = "MAF", ShortName = "Saint Martin (French part)", LongName = "The Collectivity of Saint-Martin"
               , CurrencyCode = "EUR", Languages = ["en", "fr", "nl"], UnofficialNames = ["Saint Martin", "サン・マルタン（フランス領）", "Saint-Martin", "法属圣马丁"]
               , Timezones = ["America/Marigot,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.法属圣马丁))]
    MF = 663

   ,

    /// <summary>
    ///     玻利维亚
    /// </summary>
    [CountryInfo(CallingCode = 591,    Alpha3 = "BOL", ShortName = "Bolivia (Plurinational State of)", LongName = "The Plurinational State of Bolivia"
               , CurrencyCode = "BOB", Languages = ["es", "ay", "qu"], UnofficialNames = ["Bolivia", "Bolivien", "Bolivie", "ボリビア多民族国", "玻利维亚"]
               , Timezones = ["America/La_Paz,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.玻利维亚))]
    BO = 068

   ,

    /// <summary>
    ///     圭亚那
    /// </summary>
    [CountryInfo(CallingCode = 592,  Alpha3 = "GUY", ShortName = "Guyana", LongName = "The Co-operative Republic of Guyana", CurrencyCode = "GYD"
               , Languages = ["en"], UnofficialNames = ["Guyana", "ガイアナ", "圭亚那"], Timezones = ["America/Guyana,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.圭亚那))]
    GY = 328

   ,

    /// <summary>
    ///     厄瓜多尔
    /// </summary>
    [CountryInfo(CallingCode = 593,  Alpha3 = "ECU", ShortName = "Ecuador", LongName = "The Republic of Ecuador", CurrencyCode = "USD"
               , Languages = ["es"], UnofficialNames = ["Ecuador", "Équateur", "エクアドル", "厄瓜多尔"]
               , Timezones = ["America/Guayaquil,-0500", "Pacific/Galapagos,-0600"])]
    [ResourceDescription<Ln>(nameof(Ln.厄瓜多尔))]
    EC = 218

   ,

    /// <summary>
    ///     法属圭亚那
    /// </summary>
    [CountryInfo(CallingCode = 594, Alpha3 = "GUF", ShortName = "French Guiana", LongName = "Guyane", CurrencyCode = "EUR", Languages = ["fr"]
               , UnofficialNames = ["French Guiana", "Französisch Guyana", "Guayana Francesa", "フランス領ギアナ", "Frans-Guyana", "法属圭亚那"]
               , Timezones = ["America/Cayenne,-0300"])]
    [ResourceDescription<Ln>(nameof(Ln.法属圭亚那))]
    GF = 254

   ,

    /// <summary>
    ///     巴拉圭
    /// </summary>
    [CountryInfo(CallingCode = 595,        Alpha3 = "PRY", ShortName = "Paraguay", LongName = "The Republic of Paraguay", CurrencyCode = "PYG"
               , Languages = ["es", "gn"], UnofficialNames = ["Paraguay", "パラグアイ", "巴拉圭"], Timezones = ["America/Asuncion,-0300"])]
    [ResourceDescription<Ln>(nameof(Ln.巴拉圭))]
    PY = 600

   ,

    /// <summary>
    ///     马提尼克
    /// </summary>
    [CountryInfo(CallingCode = 596, Alpha3 = "MTQ", ShortName = "Martinique", LongName = "Martinique", CurrencyCode = "EUR", Languages = ["fr"]
               , UnofficialNames = ["Martinique", "Martinica", "マルティニーク", "马提尼克"], Timezones = ["America/Martinique,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.马提尼克))]
    MQ = 474

   ,

    /// <summary>
    ///     苏里南
    /// </summary>
    [CountryInfo(CallingCode = 597,  Alpha3 = "SUR", ShortName = "Suriname", LongName = "The Republic of Suriname", CurrencyCode = "SRD"
               , Languages = ["nl"], UnofficialNames = ["Suriname", "Surinam", "スリナム", "苏里南"], Timezones = ["America/Paramaribo,-0300"])]
    [ResourceDescription<Ln>(nameof(Ln.苏里南))]
    SR = 740

   ,

    /// <summary>
    ///     乌拉圭
    /// </summary>
    [CountryInfo(CallingCode = 598,  Alpha3 = "URY", ShortName = "Uruguay", LongName = "The Oriental Republic of Uruguay", CurrencyCode = "UYU"
               , Languages = ["es"], UnofficialNames = ["Uruguay", "ウルグアイ", "乌拉圭"], Timezones = ["America/Montevideo,-0300"])]
    [ResourceDescription<Ln>(nameof(Ln.乌拉圭))]
    UY = 858

   ,

    /// <summary>
    ///     库拉索
    /// </summary>
    [CountryInfo(CallingCode = 599,    CallingSubCode = ["9"], Alpha3 = "CUW", ShortName = "Curaçao", LongName = "The Country of Curaçao"
               , CurrencyCode = "ANG", Languages = ["nl"],     UnofficialNames = ["Curaçao", "キュラソー島", "库拉索"], Timezones = ["America/Curacao,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.库拉索))]
    CW = 531

   ,

    /// <summary>
    ///     荷兰加勒比区
    /// </summary>
    [CountryInfo(CallingCode = 599,    Alpha3 = "BES", ShortName = "Bonaire, Sint Eustatius and Saba", LongName = "Bonaire, Sint Eustatius and Saba"
               , CurrencyCode = "USD", Languages = ["nl", "en"]
               , UnofficialNames = [
                     "Bonaire, Sint Eustatius and Saba", "Caribbean Netherlands", "Caribisch Nederland", "ボネール、シント・ユースタティウスおよびサバ", "荷兰加勒比区"
                 ], IsPreferred = true, Timezones = ["America/Kralendijk,-0400"])]
    [ResourceDescription<Ln>(nameof(Ln.荷兰加勒比区))]
    BQ = 535

   ,

    /// <summary>
    ///     东帝汶
    /// </summary>
    [CountryInfo(CallingCode = 670,    Alpha3 = "TLS", ShortName = "Timor-Leste", LongName = "The Democratic Republic of Timor-Leste"
               , CurrencyCode = "IDR", Languages = ["pt"]
               , UnofficialNames = ["East Timor", "Timor-Leste", "Timor oriental", "Timor Oriental", "東ティモール", "Oost-Timor", "东帝汶"]
               , Timezones = ["Asia/Dili,+0900"])]
    [ResourceDescription<Ln>(nameof(Ln.东帝汶))]
    TL = 626

   ,

    /// <summary>
    ///     南极洲
    /// </summary>
    [CountryInfo(CallingCode = 672, Alpha3 = "ATA", ShortName = "Antarctica", LongName = "Antarctica", CurrencyCode = "USD", Languages = []
               , UnofficialNames = ["Antarctica", "Antarktis", "Antarctique", "Antártida", "南極", "南极洲"], IsPreferred = true
               , Timezones = [
                     "Antarctica/Casey,+0800", "Antarctica/Davis,+0700", "Antarctica/DumontDUrville,+1000", "Antarctica/Mawson,+0500"
                   , "Antarctica/McMurdo,+1300", "Antarctica/Palmer,-0300", "Antarctica/Rothera,-0300", "Antarctica/Syowa,+0300"
                   , "Antarctica/Troll,+0000", "Antarctica/Vostok,+0500"
                 ])]
    [ResourceDescription<Ln>(nameof(Ln.南极洲))]
    AQ = 010

   ,

    /// <summary>
    ///     诺福克岛
    /// </summary>
    [CountryInfo(CallingCode = 672, CallingSubCode = ["3"], Alpha3 = "NFK", ShortName = "Norfolk Island", LongName = "The Territory of Norfolk Island"
               , CurrencyCode = "AUD", Languages = ["en"]
               , UnofficialNames = ["Norfolk Island", "Norfolkinsel", "Île de Norfolk", "Isla de Norfolk", "ノーフォーク島", "Norfolkeiland", "诺福克岛"]
               , Timezones = ["Pacific/Norfolk,+1200"])]
    [ResourceDescription<Ln>(nameof(Ln.诺福克岛))]
    NF = 574

   ,

    /// <summary>
    ///     文莱
    /// </summary>
    [CountryInfo(CallingCode = 673,    Alpha3 = "BRN",     ShortName = "Brunei Darussalam", LongName = "The Nation of Brunei, the Abode of Peace"
               , CurrencyCode = "BND", Languages = ["ms"], UnofficialNames = ["Brunei", "ブルネイ・ダルサラーム", "文莱"], Timezones = ["Asia/Brunei,+0800"])]
    [ResourceDescription<Ln>(nameof(Ln.文莱))]
    BN = 096

   ,

    /// <summary>
    ///     瑙鲁
    /// </summary>
    [CountryInfo(CallingCode = 674,        Alpha3 = "NRU", ShortName = "Nauru", LongName = "The Republic of Nauru", CurrencyCode = "AUD"
               , Languages = ["en", "na"], UnofficialNames = ["Nauru", "ナウル", "瑙鲁"], Timezones = ["Pacific/Nauru,+1200"])]
    [ResourceDescription<Ln>(nameof(Ln.瑙鲁))]
    NR = 520

   ,

    /// <summary>
    ///     巴布亚新几内亚
    /// </summary>
    [CountryInfo(CallingCode = 675,    Alpha3 = "PNG", ShortName = "Papua New Guinea", LongName = "The Independent State of Papua New Guinea"
               , CurrencyCode = "PGK", Languages = ["en"]
               , UnofficialNames = [
                     "Papua New Guinea", "Papua-Neuguinea", "Papouasie Nouvelle-Guinée", "Papúa Nueva Guinea", "パプアニューギニア", "Papoea-Nieuw-Guinea"
                   , "巴布亚新几内亚"
                 ], Timezones = ["Pacific/Bougainville,+1100", "Pacific/Port_Moresby,+1000"])]
    [ResourceDescription<Ln>(nameof(Ln.巴布亚新几内亚))]
    PG = 598

   ,

    /// <summary>
    ///     汤加
    /// </summary>
    [CountryInfo(CallingCode = 676,        Alpha3 = "TON", ShortName = "Tonga", LongName = "The Kingdom of Tonga", CurrencyCode = "TOP"
               , Languages = ["en", "to"], UnofficialNames = ["Tonga", "トンガ", "汤加"], Timezones = ["Pacific/Tongatapu,+1300"])]
    [ResourceDescription<Ln>(nameof(Ln.汤加))]
    TO = 776

   ,

    /// <summary>
    ///     所罗门群岛
    /// </summary>
    [CountryInfo(CallingCode = 677, Alpha3 = "SLB", ShortName = "Solomon Islands", LongName = "The Solomon Islands", CurrencyCode = "SBD"
               , Languages = ["en"]
               , UnofficialNames = ["Solomon Islands", "Salomonen", "Îles Salomon", "Islas Salomón", "ソロモン諸島", "Salomonseilanden", "所罗门群岛"]
               , Timezones = ["Pacific/Guadalcanal,+1100"])]
    [ResourceDescription<Ln>(nameof(Ln.所罗门群岛))]
    SB = 090

   ,

    /// <summary>
    ///     瓦努阿图
    /// </summary>
    [CountryInfo(CallingCode = 678,              Alpha3 = "VUT", ShortName = "Vanuatu", LongName = "The Republic of Vanuatu", CurrencyCode = "VUV"
               , Languages = ["bi", "en", "fr"], UnofficialNames = ["Vanuatu", "バヌアツ", "瓦努阿图"], Timezones = ["Pacific/Efate,+1100"])]
    [ResourceDescription<Ln>(nameof(Ln.瓦努阿图))]
    VU = 548

   ,

    /// <summary>
    ///     斐济
    /// </summary>
    [CountryInfo(CallingCode = 679,                    Alpha3 = "FJI", ShortName = "Fiji", LongName = "The Republic of Fiji", CurrencyCode = "FJD"
               , Languages = ["en", "fj", "hi", "ur"], UnofficialNames = ["Fiji", "Fidschi", "Fidji", "フィジー", "斐济"]
               , Timezones = ["Pacific/Fiji,+1200"])]
    [ResourceDescription<Ln>(nameof(Ln.斐济))]
    FJ = 242

   ,

    /// <summary>
    ///     帕劳
    /// </summary>
    [CountryInfo(CallingCode = 680, Alpha3 = "PLW", ShortName = "Palau", LongName = "The Republic of Palau", CurrencyCode = "USD", Languages = ["en"]
               , UnofficialNames = ["Palau", "パラオ", "帕劳"], Timezones = ["Pacific/Palau,+0900"])]
    [ResourceDescription<Ln>(nameof(Ln.帕劳))]
    PW = 585

   ,

    /// <summary>
    ///     瓦利斯和富图纳
    /// </summary>
    [CountryInfo(CallingCode = 681,    Alpha3 = "WLF", ShortName = "Wallis and Futuna", LongName = "The Territory of the Wallis and Futuna Islands"
               , CurrencyCode = "XPF", Languages = ["fr"]
               , UnofficialNames = [
                     "Wallis and Futuna", "Wallis und Futuna", "Wallis et Futuna", "Wallis y Futuna", "ウォリス・フツナ", "Wallis en Futuna", "瓦利斯和富图纳"
                 ], Timezones = ["Pacific/Wallis,+1200"])]
    [ResourceDescription<Ln>(nameof(Ln.瓦利斯和富图纳))]
    WF = 876

   ,

    /// <summary>
    ///     库克群岛
    /// </summary>
    [CountryInfo(CallingCode = 682,  Alpha3 = "COK", ShortName = "Cook Islands", LongName = "The Cook Islands", CurrencyCode = "NZD"
               , Languages = ["en"], UnofficialNames = ["Cook Islands", "Cookinseln", "Îles Cook", "Islas Cook", "クック諸島", "Cookeilanden", "库克群岛"]
               , Timezones = ["Pacific/Rarotonga,-1000"])]
    [ResourceDescription<Ln>(nameof(Ln.库克群岛))]
    CK = 184

   ,

    /// <summary>
    ///     纽埃
    /// </summary>
    [CountryInfo(CallingCode = 683, Alpha3 = "NIU", ShortName = "Niue", LongName = "Niue", CurrencyCode = "NZD", Languages = ["en"]
               , UnofficialNames = ["Niue", "ニウエ", "纽埃"], Timezones = ["Pacific/Niue,-1100"])]
    [ResourceDescription<Ln>(nameof(Ln.纽埃))]
    NU = 570

   ,

    /// <summary>
    ///     萨摩亚
    /// </summary>
    [CountryInfo(CallingCode = 685,        Alpha3 = "WSM", ShortName = "Samoa", LongName = "The Independent State of Samoa", CurrencyCode = "WST"
               , Languages = ["sm", "en"], UnofficialNames = ["Samoa", "サモア", "萨摩亚"], Timezones = ["Pacific/Apia,+1300"])]
    [ResourceDescription<Ln>(nameof(Ln.萨摩亚))]
    WS = 882

   ,

    /// <summary>
    ///     基里巴斯
    /// </summary>
    [CountryInfo(CallingCode = 686,  Alpha3 = "KIR", ShortName = "Kiribati", LongName = "The Republic of Kiribati", CurrencyCode = "AUD"
               , Languages = ["en"], UnofficialNames = ["Kiribati", "キリバス", "基里巴斯"]
               , Timezones = ["Pacific/Kanton,+1300", "Pacific/Kiritimati,+1400", "Pacific/Tarawa,+1200"])]
    [ResourceDescription<Ln>(nameof(Ln.基里巴斯))]
    KI = 296

   ,

    /// <summary>
    ///     新喀里多尼亚
    /// </summary>
    [CountryInfo(CallingCode = 687, Alpha3 = "NCL", ShortName = "New Caledonia", LongName = "New Caledonia", CurrencyCode = "XPF", Languages = ["fr"]
               , UnofficialNames = [
                     "New Caledonia", "Neukaledonien", "Nouvelle-Calédonie", "Nueva Caledonia", "ニューカレドニア", "Nieuw-Caledonië", "新喀里多尼亚"
                 ], Timezones = ["Pacific/Noumea,+1100"])]
    [ResourceDescription<Ln>(nameof(Ln.新喀里多尼亚))]
    NC = 540

   ,

    /// <summary>
    ///     图瓦卢
    /// </summary>
    [CountryInfo(CallingCode = 688, Alpha3 = "TUV", ShortName = "Tuvalu", LongName = "Tuvalu", CurrencyCode = "AUD", Languages = ["en"]
               , UnofficialNames = ["Tuvalu", "ツバル", "图瓦卢"], Timezones = ["Pacific/Funafuti,+1200"])]
    [ResourceDescription<Ln>(nameof(Ln.图瓦卢))]
    TV = 798

   ,

    /// <summary>
    ///     法属波利尼西亚
    /// </summary>
    [CountryInfo(CallingCode = 689, Alpha3 = "PYF", ShortName = "French Polynesia", LongName = "French Polynesia", CurrencyCode = "XPF"
               , Languages = ["fr"]
               , UnofficialNames = [
                     "French Polynesia", "Französisch-Polynesien", "Polynésie Française", "Polinesia Francesa", "フランス領ポリネシア", "Frans-Polynesië"
                   , "法属波利尼西亚"
                 ], Timezones = ["Pacific/Gambier,-0900", "Pacific/Marquesas,-0930", "Pacific/Tahiti,-1000"])]
    [ResourceDescription<Ln>(nameof(Ln.法属波利尼西亚))]
    PF = 258

   ,

    /// <summary>
    ///     托克劳
    /// </summary>
    [CountryInfo(CallingCode = 690, Alpha3 = "TKL", ShortName = "Tokelau", LongName = "Tokelau", CurrencyCode = "NZD", Languages = ["en"]
               , UnofficialNames = ["Tokelau", "Îles Tokelau", "Islas Tokelau", "トケラウ", "托克劳"], Timezones = ["Pacific/Fakaofo,+1300"])]
    [ResourceDescription<Ln>(nameof(Ln.托克劳))]
    TK = 772

   ,

    /// <summary>
    ///     密克罗尼西亚联邦
    /// </summary>
    [CountryInfo(CallingCode = 691,    Alpha3 = "FSM", ShortName = "Micronesia (Federated States of)", LongName = "The Federated States of Micronesia"
               , CurrencyCode = "USD", Languages = ["en"]
               , UnofficialNames = ["Micronesia", "Mikronesien", "Micronésie", "ミクロネシア連邦", "Micronesië", "密克罗尼西亚联邦"]
               , Timezones = ["Pacific/Chuuk,+1000", "Pacific/Kosrae,+1100", "Pacific/Pohnpei,+1100"])]
    [ResourceDescription<Ln>(nameof(Ln.密克罗尼西亚联邦))]
    FM = 583

   ,

    /// <summary>
    ///     马绍尔群岛
    /// </summary>
    [CountryInfo(CallingCode = 692,    Alpha3 = "MHL", ShortName = "Marshall Islands", LongName = "The Republic of the Marshall Islands"
               , CurrencyCode = "USD", Languages = ["en", "mh"]
               , UnofficialNames = ["Marshall Islands", "Marshallinseln", "Îles Marshall", "Islas Marshall", "マーシャル諸島", "Marshalleilanden", "马绍尔群岛"]
               , Timezones = ["Pacific/Kwajalein,+1200", "Pacific/Majuro,+1200"])]
    [ResourceDescription<Ln>(nameof(Ln.马绍尔群岛))]
    MH = 584

   ,

    /// <summary>
    ///     朝鲜
    /// </summary>
    [CountryInfo(CallingCode = 850,                                      Alpha3 = "PRK",       ShortName = "Korea (Democratic People's Republic of)"
               , LongName = "The Democratic People's Republic of Korea", CurrencyCode = "KPW", Languages = ["ko"]
               , UnofficialNames = [
                     "Korea (North)", "North Korea", "Nordkorea", "Corée du Nord", "Corea del Norte", "朝鮮民主主義人民共和国", "Noord-Korea"
                   , "Korea Democratic People's Republic", "Korea (Democratic People s Republic of)", "朝鲜"
                 ], Timezones = ["Asia/Pyongyang,+0900"])]
    [ResourceDescription<Ln>(nameof(Ln.朝鲜))]
    KP = 408

   ,

    /// <summary>
    ///     香港
    /// </summary>
    [CountryInfo(CallingCode = 852,    Alpha3 = "HKG", ShortName = "Hong Kong", LongName = "The Hong Kong Special Administrative Region of China"
               , CurrencyCode = "HKD", Languages = ["en", "zh"], UnofficialNames = ["Hong Kong", "香港", "Hongkong"]
               , Timezones = ["Asia/Hong_Kong,+0800"])]
    [ResourceDescription<Ln>(nameof(Ln.香港))]
    HK = 344

   ,

    /// <summary>
    ///     澳门
    /// </summary>
    [CountryInfo(CallingCode = 853,    Alpha3 = "MAC",           ShortName = "Macao", LongName = "The Macao Special Administrative Region of China"
               , CurrencyCode = "MOP", Languages = ["zh", "pt"], UnofficialNames = ["Macao", "Macau", "マカオ", "澳门"], Timezones = ["Asia/Macau,+0800"])]
    [ResourceDescription<Ln>(nameof(Ln.澳门))]
    MO = 446

   ,

    /// <summary>
    ///     柬埔寨
    /// </summary>
    [CountryInfo(CallingCode = 855,  Alpha3 = "KHM", ShortName = "Cambodia", LongName = "The Kingdom of Cambodia", CurrencyCode = "KHR"
               , Languages = ["km"], UnofficialNames = ["Cambodia", "Kambodscha", "Cambodge", "Camboya", "カンボジア", "Cambodja", "柬埔寨"]
               , Timezones = ["Asia/Phnom_Penh,+0700"])]
    [ResourceDescription<Ln>(nameof(Ln.柬埔寨))]
    KH = 116

   ,

    /// <summary>
    ///     老挝
    /// </summary>
    [CountryInfo(CallingCode = 856, Alpha3 = "LAO", ShortName = "Lao People's Democratic Republic", LongName = "The Lao People's Democratic Republic"
               , CurrencyCode = "LAK", Languages = ["lo"], UnofficialNames = ["Laos", "ラオス人民民主共和国", "Lao People s Democratic Republic", "老挝"]
               , Timezones = ["Asia/Vientiane,+0700"])]
    [ResourceDescription<Ln>(nameof(Ln.老挝))]
    LA = 418

   ,

    /// <summary>
    ///     孟加拉国
    /// </summary>
    [CountryInfo(CallingCode = 880,  Alpha3 = "BGD", ShortName = "Bangladesh", LongName = "The People's Republic of Bangladesh", CurrencyCode = "BDT"
               , Languages = ["bn"], UnofficialNames = ["Bangladesh", "Bangladesch", "バングラデシュ", "孟加拉国"], Timezones = ["Asia/Dhaka,+0600"])]
    [ResourceDescription<Ln>(nameof(Ln.孟加拉国))]
    BD = 050

   ,

    /// <summary>
    ///     台湾
    /// </summary>
    [CountryInfo(CallingCode = 886,    Alpha3 = "TWN",     ShortName = "Taiwan, Province of China", LongName = "Taiwan, Province of China"
               , CurrencyCode = "TWD", Languages = ["zh"], UnofficialNames = ["Taiwan", "Taiwán", "台灣", "臺灣", "台湾"]
               , Timezones = ["Asia/Taipei,+0800"])]
    [ResourceDescription<Ln>(nameof(Ln.台湾))]
    TW = 158

   ,

    /// <summary>
    ///     马尔代夫
    /// </summary>
    [CountryInfo(CallingCode = 960,  Alpha3 = "MDV", ShortName = "Maldives", LongName = "The Republic of Maldives", CurrencyCode = "MVR"
               , Languages = ["dv"], UnofficialNames = ["Maldives", "Malediven", "Maldivas", "モルディブ", "Maldiven", "马尔代夫"]
               , Timezones = ["Indian/Maldives,+0500"])]
    [ResourceDescription<Ln>(nameof(Ln.马尔代夫))]
    MV = 462

   ,

    /// <summary>
    ///     黎巴嫩
    /// </summary>
    [CountryInfo(CallingCode = 961,        Alpha3 = "LBN", ShortName = "Lebanon", LongName = "The Lebanese Republic", CurrencyCode = "LBP"
               , Languages = ["ar", "fr"], UnofficialNames = ["Lebanon", "لبنان", "Libanon", "Liban", "Líbano", "レバノン", "黎巴嫩"]
               , Timezones = ["Asia/Beirut,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.黎巴嫩))]
    LB = 422

   ,

    /// <summary>
    ///     约旦
    /// </summary>
    [CountryInfo(CallingCode = 962,  Alpha3 = "JOR", ShortName = "Jordan", LongName = "The Hashemite Kingdom of Jordan", CurrencyCode = "JOD"
               , Languages = ["ar"], UnofficialNames = ["Jordan", "الأردن", "Jordanien", "Jordanie", "Jordania", "ヨルダン", "Jordanië", "约旦"]
               , Timezones = ["Asia/Amman,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.约旦))]
    JO = 400

   ,

    /// <summary>
    ///     叙利亚
    /// </summary>
    [CountryInfo(CallingCode = 963,  Alpha3 = "SYR", ShortName = "Syrian Arab Republic", LongName = "The Syrian Arab Republic", CurrencyCode = "SYP"
               , Languages = ["ar"], UnofficialNames = ["Syria", "سوريا", "سورية", "Syrien", "Syrie", "Siria", "シリア・アラブ共和国", "Syrië", "叙利亚"]
               , Timezones = ["Asia/Damascus,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.叙利亚))]
    SY = 760

   ,

    /// <summary>
    ///     伊拉克
    /// </summary>
    [CountryInfo(CallingCode = 964, Alpha3 = "IRQ", ShortName = "Iraq", LongName = "The Republic of Iraq", CurrencyCode = "IQD", Languages = ["ar"]
               , UnofficialNames = ["Iraq", "العراق", "Irak", "イラク", "伊拉克"], Timezones = ["Asia/Baghdad,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.伊拉克))]
    IQ = 368

   ,

    /// <summary>
    ///     科威特
    /// </summary>
    [CountryInfo(CallingCode = 965, Alpha3 = "KWT", ShortName = "Kuwait", LongName = "The State of Kuwait", CurrencyCode = "KWD", Languages = ["ar"]
               , UnofficialNames = ["Kuwait", "الكويت", "Koweït", "クウェート", "Koeweit", "科威特"], Timezones = ["Asia/Kuwait,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.科威特))]
    KW = 414

   ,

    /// <summary>
    ///     沙特阿拉伯
    /// </summary>
    [CountryInfo(CallingCode = 966, Alpha3 = "SAU", ShortName = "Saudi Arabia", LongName = "The Kingdom of Saudi Arabia", CurrencyCode = "SAR"
               , Languages = ["ar"]
               , UnofficialNames = [
                     "Saudi Arabia", "Kingdom of Saudi Arabia", "السعودية", "Saudi-Arabien", "Arabie Saoudite", "Arabia Saudí", "サウジアラビア"
                   , "Saoedi-Arabië", "沙特阿拉伯"
                 ], Timezones = ["Asia/Riyadh,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.沙特阿拉伯))]
    SA = 682

   ,

    /// <summary>
    ///     也门
    /// </summary>
    [CountryInfo(CallingCode = 967, Alpha3 = "YEM", ShortName = "Yemen", LongName = "The Republic of Yemen", CurrencyCode = "YER", Languages = ["ar"]
               , UnofficialNames = ["Yemen", "اليمن", "Jemen", "Yémen", "イエメン", "也门"], Timezones = ["Asia/Aden,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.也门))]
    YE = 887

   ,

    /// <summary>
    ///     阿曼
    /// </summary>
    [CountryInfo(CallingCode = 968, Alpha3 = "OMN", ShortName = "Oman", LongName = "The Sultanate of Oman", CurrencyCode = "OMR", Languages = ["ar"]
               , UnofficialNames = ["Oman", "عمان", "Omán", "オマーン", "阿曼"], Timezones = ["Asia/Muscat,+0400"])]
    [ResourceDescription<Ln>(nameof(Ln.阿曼))]
    OM = 512

   ,

    /// <summary>
    ///     巴勒斯坦
    /// </summary>
    [CountryInfo(CallingCode = 970, Alpha3 = "PSE", ShortName = "Palestine, State of", LongName = "The State of Palestine", CurrencyCode = "ILS"
               , Languages = ["ar", "he", "en"]
               , UnofficialNames = [
                     "Palestine", "فلسطين", "Palästina", "Palestina", "the Occupied Palestinian Territory", "パレスチナ", "Palestijnse gebieden"
                   , "Palestinian Territory Occupied", "Palestinian Authority", "巴勒斯坦"
                 ], Timezones = ["Asia/Gaza,+0200", "Asia/Hebron,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.巴勒斯坦))]
    PS = 275

   ,

    /// <summary>
    ///     阿联酋
    /// </summary>
    [CountryInfo(CallingCode = 971, Alpha3 = "ARE", ShortName = "United Arab Emirates", LongName = "The United Arab Emirates", CurrencyCode = "AED"
               , Languages = ["ar"]
               , UnofficialNames = [
                     "United Arab Emirates", "الإمارات العربية المتحدة", "Vereinigte Arabische Emirate", "Émirats Arabes Unis"
                   , "Emiratos Árabes Unidos", "アラブ首長国連邦", "Verenigde Arabische Emiraten", "阿联酋"
                 ], Timezones = ["Asia/Dubai,+0400"])]
    [ResourceDescription<Ln>(nameof(Ln.阿联酋))]
    AE = 784

   ,

    /// <summary>
    ///     以色列
    /// </summary>
    [CountryInfo(CallingCode = 972,        Alpha3 = "ISR", ShortName = "Israel", LongName = "The State of Israel", CurrencyCode = "ILS"
               , Languages = ["he", "ar"], UnofficialNames = ["Israel", "Israël", "イスラエル", "以色列"], Timezones = ["Asia/Jerusalem,+0200"])]
    [ResourceDescription<Ln>(nameof(Ln.以色列))]
    IL = 376

   ,

    /// <summary>
    ///     巴林
    /// </summary>
    [CountryInfo(CallingCode = 973,  Alpha3 = "BHR", ShortName = "Bahrain", LongName = "The Kingdom of Bahrain", CurrencyCode = "BHD"
               , Languages = ["ar"], UnofficialNames = ["Bahrain", "البحرين", "Bahreïn", "Bahrein", "バーレーン", "巴林"]
               , Timezones = ["Asia/Bahrain,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.巴林))]
    BH = 048

   ,

    /// <summary>
    ///     卡塔尔
    /// </summary>
    [CountryInfo(CallingCode = 974, Alpha3 = "QAT", ShortName = "Qatar", LongName = "The State of Qatar", CurrencyCode = "QAR", Languages = ["ar"]
               , UnofficialNames = ["Qatar", "قطر", "Katar", "カタール", "卡塔尔"], Timezones = ["Asia/Qatar,+0300"])]
    [ResourceDescription<Ln>(nameof(Ln.卡塔尔))]
    QA = 634

   ,

    /// <summary>
    ///     不丹
    /// </summary>
    [CountryInfo(CallingCode = 975, Alpha3 = "BTN", ShortName = "Bhutan", LongName = "The Kingdom of Bhutan", CurrencyCode = "BTN", Languages = ["dz"]
               , UnofficialNames = ["Bhutan", "Bhoutan", "Bután", "ブータン", "不丹"], Timezones = ["Asia/Thimphu,+0600"])]
    [ResourceDescription<Ln>(nameof(Ln.不丹))]
    BT = 064

   ,

    /// <summary>
    ///     蒙古
    /// </summary>
    [CountryInfo(CallingCode = 976, Alpha3 = "MNG", ShortName = "Mongolia", LongName = "Mongolia", CurrencyCode = "MNT", Languages = ["mn"]
               , UnofficialNames = ["Mongolia", "Mongolei", "Mongolie", "モンゴル", "Mongolië", "蒙古"]
               , Timezones = ["Asia/Hovd,+0700", "Asia/Ulaanbaatar,+0800"])]
    [ResourceDescription<Ln>(nameof(Ln.蒙古))]
    MN = 496

   ,

    /// <summary>
    ///     尼泊尔
    /// </summary>
    [CountryInfo(CallingCode = 977, Alpha3 = "NPL", ShortName = "Nepal", LongName = "The Federal Democratic Republic of Nepal", CurrencyCode = "NPR"
               , Languages = ["ne", "mai", "bho", "new", "urd"]
               , UnofficialNames = ["Nepal", "Népal", "the Federal Democratic Republic of Nepal", "ネパール", "尼泊尔"]
               , Timezones = ["Asia/Kathmandu,+0545"])]
    [ResourceDescription<Ln>(nameof(Ln.尼泊尔))]
    NP = 524

   ,

    /// <summary>
    ///     塔吉克斯坦
    /// </summary>
    [CountryInfo(CallingCode = 992, Alpha3 = "TJK", ShortName = "Tajikistan", LongName = "The Republic of Tajikistan", CurrencyCode = "TJS"
               , Languages = ["tg", "ru"]
               , UnofficialNames = ["Tajikistan", "Tadschikistan", "Tayikistán", "タジキスタン", "Tadzjikistan", "Tajikstan", "塔吉克斯坦"]
               , Timezones = ["Asia/Dushanbe,+0500"])]
    [ResourceDescription<Ln>(nameof(Ln.塔吉克斯坦))]
    TJ = 762

   ,

    /// <summary>
    ///     土库曼斯坦
    /// </summary>
    [CountryInfo(CallingCode = 993,        Alpha3 = "TKM", ShortName = "Turkmenistan", LongName = "Turkmenistan", CurrencyCode = "TMT"
               , Languages = ["tk", "ru"], UnofficialNames = ["Turkmenistan", "Turkménistan", "Turkmenistán", "トルクメニスタン", "Turkmenia", "土库曼斯坦"]
               , Timezones = ["Asia/Ashgabat,+0500"])]
    [ResourceDescription<Ln>(nameof(Ln.土库曼斯坦))]
    TM = 795

   ,

    /// <summary>
    ///     阿塞拜疆
    /// </summary>
    [CountryInfo(CallingCode = 994, Alpha3 = "AZE", ShortName = "Azerbaijan", LongName = "The Republic of Azerbaijan", CurrencyCode = "AZN"
               , Languages = ["az", "hy"]
               , UnofficialNames = ["Azerbaijan", "Aserbaidschan", "Azerbaïdjan", "Azerbaiyán", "アゼルバイジャン", "Azerbeidzjan", "阿塞拜疆"]
               , Timezones = ["Asia/Baku,+0400"])]
    [ResourceDescription<Ln>(nameof(Ln.阿塞拜疆))]
    AZ = 031

   ,

    /// <summary>
    ///     格鲁吉亚
    /// </summary>
    [CountryInfo(CallingCode = 995, Alpha3 = "GEO", ShortName = "Georgia", LongName = "Georgia", CurrencyCode = "GEL", Languages = ["ka"]
               , UnofficialNames = ["Georgia", "Georgien", "Géorgie", "グルジア", "Georgië", "格鲁吉亚"], Timezones = ["Asia/Tbilisi,+0400"])]
    [ResourceDescription<Ln>(nameof(Ln.格鲁吉亚))]
    GE = 268

   ,

    /// <summary>
    ///     吉尔吉斯斯坦
    /// </summary>
    [CountryInfo(CallingCode = 996, Alpha3 = "KGZ", ShortName = "Kyrgyzstan", LongName = "The Kyrgyz Republic", CurrencyCode = "KGS"
               , Languages = ["ky", "ru"]
               , UnofficialNames = ["Kyrgyzstan", "Kirgisistan", "Kirghizistan", "Kirguizistán", "キルギス", "Kirgizië", "Kyrgzstan", "吉尔吉斯斯坦"]
               , Timezones = ["Asia/Bishkek,+0600"])]
    [ResourceDescription<Ln>(nameof(Ln.吉尔吉斯斯坦))]
    KG = 417

   ,

    /// <summary>
    ///     乌兹别克斯坦
    /// </summary>
    [CountryInfo(CallingCode = 998, Alpha3 = "UZB", ShortName = "Uzbekistan", LongName = "The Republic of Uzbekistan", CurrencyCode = "UZS"
               , Languages = ["uz", "ru"]
               , UnofficialNames = ["Uzbekistan", "Usbekistan", "Ouzbékistan", "Uzbekistán", "ウズベキスタン", "Oezbekistan", "乌兹别克斯坦"]
               , Timezones = ["Asia/Samarkand,+0500", "Asia/Tashkent,+0500"])]
    [ResourceDescription<Ln>(nameof(Ln.乌兹别克斯坦))]
    UZ = 860
}