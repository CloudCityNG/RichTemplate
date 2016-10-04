<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <table>
      <xsl select="rss/channel/item">
        <tr>
          <td>
            <xsl:element name="a">
              <xsl:attribute name="href">
                <xsl:value-of select="link"/>
              </xsl:attribute>
              <xsl:value-of select="description"/>
            </xsl:element>
          </td>
        </tr>
      </xsl>
    </table>
  </xsl:template>
</xsl:stylesheet>
