<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
                xmlns:ns="http://travelorder.example.com/sipvs" 
                exclude-result-prefixes="ns">
    <xsl:output method="html" encoding="UTF-8" />

    <xsl:template match="/">
        <html>
            <head>
                <title>Cestovný príkaz</title>
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        margin: 20px;
                    }
                    table {
                        width: 100%;
                        border-collapse: collapse;
                        margin-bottom: 20px;
                    }
                    table, th, td {
                        border: 1px solid black;
                        padding: 5px;
                    }
                </style>
            </head>
            <body>
                <h1>Cestovný príkaz</h1>
                <table>
                    <tr>
                        <th>Zamestnávateľ</th>
                        <td><xsl:value-of select="ns:CestovnyPrikaz/ns:Zamestnavatel"/></td>
                    </tr>
                    <tr>
                        <th>Priezvisko, meno, titul</th>
                        <td><xsl:value-of select="ns:CestovnyPrikaz/ns:PriezviskoMenoTitul"/></td>
                    </tr>
                    <tr>
                        <th>Bydlisko</th>
                        <td><xsl:value-of select="ns:CestovnyPrikaz/ns:Bydlisko"/></td>
                    </tr>
                    <tr>
                        <th>Osobné číslo</th>
                        <td><xsl:value-of select="ns:CestovnyPrikaz/ns:OsobneCislo"/></td>
                    </tr>
                    <tr>
                        <th>Útvar</th>
                        <td><xsl:value-of select="ns:CestovnyPrikaz/ns:Utvar"/></td>
                    </tr>
                    <tr>
                        <th>Telefón</th>
                        <td><xsl:value-of select="ns:CestovnyPrikaz/ns:Telefon"/></td>
                    </tr>
                    <tr>
                        <th>Normálny pracovný čas</th>
                        <td>
                            <xsl:value-of select="ns:CestovnyPrikaz/ns:PracovnyCas"/> hodín, od 
                            <xsl:value-of select="ns:CestovnyPrikaz/ns:Od"/> do 
                            <xsl:value-of select="ns:CestovnyPrikaz/ns:Do"/>
                        </td>
                    </tr>
                </table>

                <h2>Cesty</h2>
                <table>
                    <xsl:for-each select="ns:CestovnyPrikaz/ns:Cesty/ns:Cesta">
                        <tr>
                            <th>Začiatok cesty</th>
                            <td><xsl:value-of select="ns:ZaciatokCesty"/></td>
                        </tr>
                        <tr>
                            <th>Mesto rokovania</th>
                            <td><xsl:value-of select="ns:MestoRokovania"/></td>
                        </tr>
                        <tr>
                            <th>Účel cesty</th>
                            <td><xsl:value-of select="ns:UcelCesty"/></td>
                        </tr>
                        <tr>
                            <th>Koniec cesty</th>
                            <td><xsl:value-of select="ns:KoniecCesty"/></td>
                        </tr>
                    </xsl:for-each>
                </table>

                <h2>Podpisy</h2>
                <table>
                    <tr>
                        <th>Podpis pokladníka</th>
                        <td><xsl:value-of select="ns:CestovnyPrikaz/ns:PodpisPokladnika"/></td>
                    </tr>
                    <tr>
                        <th>Dátum podpisu pokladníka</th>
                        <td><xsl:value-of select="ns:CestovnyPrikaz/ns:DatumPodpisPokladnika"/></td>
                    </tr>
                </table>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>
