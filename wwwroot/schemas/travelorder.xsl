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
                        background-color: #282828;
                        color: white;
                        padding: 20px;
                    }
                    .section {
                        margin-bottom: 20px;
                        border: 1px solid #444;
                        padding: 20px;
                        border-radius: 5px;
                        background-color: #333;
                    }
                    .section h2 {
                        color: #fff;
                        font-size: 18px;
                        margin-bottom: 10px;
                        border-bottom: 1px solid #555;
                        padding-bottom: 5px;
                    }
                    .row {
                        display: flex;
                        justify-content: space-between;
                        margin-bottom: 10px;
                        gap: 15px; 
                    }
                    .row div {
                        width: 100%;

                    }
                    label {
                        font-weight: bold;
                        color: #aaa;
                    }
                    input, .value {
                        width: 100%;
                        padding: 8px;
                        background-color: #444;
                        border: none;
                        color: #fff;
                        box-sizing: border-box;
                    }
                    .value {
                        padding: 8px;
                    }
                </style>
            </head>
            <body>
                <h1>Cestovný PRÍKAZ</h1>
                
                <div class="section">
                    <div class="row">
                        <div>
                            <label>Zamestnávateľ</label>
                            <div class="value"><xsl:value-of select="ns:CestovnyPrikaz/ns:Zamestnavatel"/></div>
                        </div>
                        <div>
                            <label>Osobné číslo</label>
                            <div class="value"><xsl:value-of select="ns:CestovnyPrikaz/ns:OsobneCislo"/></div>
                        </div>
                    </div>
                    <div class="row">
                        <div>
                            <label>Priezvisko, meno, titul</label>
                            <div class="value"><xsl:value-of select="ns:CestovnyPrikaz/ns:PriezviskoMenoTitul"/></div>
                        </div>
                        <div>
                            <label>Útvar</label>
                            <div class="value"><xsl:value-of select="ns:CestovnyPrikaz/ns:Utvar"/></div>
                        </div>
                    </div>
                    <div class="row">
                        <div>
                            <label>Bydlisko</label>
                            <div class="value"><xsl:value-of select="ns:CestovnyPrikaz/ns:Bydlisko"/></div>
                        </div>
                        <div>
                            <label>Telefón, klapka</label>
                            <div class="value"><xsl:value-of select="ns:CestovnyPrikaz/ns:Telefon"/></div>
                        </div>
                    </div>
                    <div class="row">
                        <div>
                            <label>Normálny pracovný čas</label>
                            <div class="value"><xsl:value-of select="ns:CestovnyPrikaz/ns:PracovnyCas"/> hodín, od <xsl:value-of select="ns:CestovnyPrikaz/ns:Od"/> do <xsl:value-of select="ns:CestovnyPrikaz/ns:Do"/></div>
                        </div>
                    </div>
                </div>

                <div class="section">
                    
                    <xsl:for-each select="ns:CestovnyPrikaz/ns:Cesty/ns:Cesta">
                        <h2>Cesta</h2>
                        <div class="row">
                            <div>
                                <label>Začiatok cesty (miesto, dátum, hodina)</label>
                                <div class="value"><xsl:value-of select="ns:ZaciatokCesty"/></div>
                            </div>
                        </div>
                        <div class="row">
                            <div>
                                <label>Mesto rokovania</label>
                                <div class="value"><xsl:value-of select="ns:MestoRokovania"/></div>
                            </div>
                        </div>
                        <div class="row">
                            <div>
                                <label>Účel cesty</label>
                                <div class="value"><xsl:value-of select="ns:UcelCesty"/></div>
                            </div>
                        </div>
                        <div class="row">
                            <div>
                                <label>Koniec cesty (miesto, dátum)</label>
                                <div class="value"><xsl:value-of select="ns:KoniecCesty"/></div>
                            </div>
                        </div>
                    </xsl:for-each>
                </div>

                <div class="section">
                    <h2>Doplnkové informácie</h2>
                    <div class="row">
                        <div>
                            <label>Spolucestujúci</label>
                            <div class="value"><xsl:value-of select="ns:CestovnyPrikaz/ns:Spolucestujuci"/></div>
                        </div>
                    </div>
                    <div class="row">
                        <div>
                            <label>Určený dopravný prostriedok</label>
                            <div class="value"><xsl:value-of select="ns:CestovnyPrikaz/ns:DopravnyProstriedok"/></div>
                        </div>
                    </div>
                    <div class="row">
                        <div>
                            <label>Predpokladaná čiastka výdavkov v EUR</label>
                            <div class="value"><xsl:value-of select="ns:CestovnyPrikaz/ns:CiastkaVydavkov"/></div>
                        </div>
                    </div>
                    <div class="row">
                        <div>
                            <label>Povolený preddavok v EUR</label>
                            <div class="value"><xsl:value-of select="ns:CestovnyPrikaz/ns:Preddavok"/></div>
                        </div>
                    </div>
                </div>

                <div class="section">
                    <h2>Podpisy</h2>
                    <div class="column">
                        <div>
                            <label>Podpis pokladníka</label>
                            <div class="value"><xsl:value-of select="ns:CestovnyPrikaz/ns:PodpisPokladnika"/></div>
                        </div>
                        <div>
                            <label>Dátum podpisu pokladníka</label>
                            <div class="value"><xsl:value-of select="ns:CestovnyPrikaz/ns:DatumPodpisPokladnika"/></div>
                        </div>
                    </div>
                </div>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>
