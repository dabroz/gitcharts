gitcharts v 0.2 (upcoming)
==========================

gitcharts is a C# applications that can be used to generate Lines-of-code statistics using git repository data. It's similar to statsvn LOC chart. gitcharts can track multiple projects inside repository (each project is drawn using different colour).

Example screenshot:

[![Screenshot](http://dabroz.scythe.pl/upload/2010/10/chart2.jpg)](http://dabroz.scythe.pl/upload/2010/10/chart2.jpg) 

Changelog
---------

*0.2:*

* moved from custom graph "library" to GNUPLOT (I was also expertimenting with ZedGraph)
* two-level directories
* more customization options available

Roadmap
-------

0.2: few fixes needed (mostly chart related)
0.3: caching
1.0: rewrite gitcharts into portable ANSI C instead of C#
