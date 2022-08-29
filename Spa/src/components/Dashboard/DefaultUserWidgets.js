// Refer to "https://jbaysolutions.github.io/vue-grid-layout/guide/properties.html" for more details
export default [
  {
    "x": 0,
    "y": 0,
    "w": 3,
    "h": 6,
    "minW": 3,
    "minH": 3,
    "i": 2,
    "component": "VulnListComponent",
    "payload": {
        "page": 1,
        "issuers[0]": "CNA-2005-0005"
    },
    "title": "Microsoft issued vulnerabilities",
    "searchPage": false,
    "moved": false
  },
  {
  "x": 4,
  "y": 0,
  "w": 3,
  "h": 2,
  "minW": 3,
  "minH": 2,
  "i": 5,
  "component": "ChartLoaderComponent",
  "features": [
      "Vulnerabilities-Cardinality"
  ],
  "payload": {
      "Field": "Vulnerabilities",
      "Type": "Cardinality",
      "issuers[0]": "CNA-2009-0002"
  },
  "chartComponent": "ScalerChartComponent",
  "title": "Current unique number of Apple issued vulnerabilities",
  "aggregationLength": 1,
  "moved": false
  },
  {
  "x": 7,
  "y": 0,
  "w": 3,
  "h": 2,
  "minW": 3,
  "minH": 2,
  "i": 6,
  "component": "ChartLoaderComponent",
  "features": [
      "Weakness-Cardinality"
  ],
  "payload": {
      "Field": "Weakness",
      "Type": "Cardinality",
      "vulnerableProducts[0].vendor": "Linux",
      "vulnerableProducts[0].product": "Linux Kernel"
  },
  "chartComponent": "ScalerChartComponent",
  "title": "Current unique number of weaknesses in Linux Kernel",
  "aggregationLength": 1,
  "moved": false
  },
  {
  "x": 11,
  "y": 0,
  "w": 5,
  "h": 3,
  "minW": 3,
  "minH": 3,
  "i": 7,
  "component": "ChartLoaderComponent",
  "features": [
      "SeverityLevel",
      "PublishDateRange",
      "PublishDateRange Count"
  ],
  "payload": {
      "SubAggregation.Field": "PublishDateRange",
      "SubAggregation.Type": "DateHistogram",
      "SubAggregation.TypeValue": "Year",
      "Field": "SeverityLevel",
      "Type": "Terms",
      "issuers[0]": "CNA-1999-0001"
  },
  "chartComponent": "MultiLineChartComponent",
  "title": "MITRE issued vulnerabilities based on severity level",
  "aggregationLength": 2,
  "moved": false
},
{
  "x": 11,
  "y": 3,
  "w": 5,
  "h": 3,
  "minW": 3,
  "minH": 3,
  "i": 8,
  "component": "ChartLoaderComponent",
  "features": [
      "IssuerName",
      "Vulnerabilities"
  ],
  "payload": {
      "Field": "IssuerName",
      "Type": "Terms",
      "TypeValue": 5
  },
  "chartComponent": "PieChartComponent",
  "title": "Pie chart of the top 5 vulnerability issuers",
  "aggregationLength": 1,
  "moved": false
},
{
  "x": 4,
  "y": 2,
  "w": 6,
  "h": 4,
  "minW": 3,
  "minH": 3,
  "i": 10,
  "component": "ChartLoaderComponent",
  "features": [
      "SeverityRange",
      "Vulnerabilities"
  ],
  "payload": {
      "Field": "SeverityRange",
      "Type": "Range"
  },
  "chartComponent": "BarChartComponent",
  "title": "Number of vulnerabilities based on severity level",
  "aggregationLength": 1,
  "moved": false
}
];
