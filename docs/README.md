# ⚙️ PSS — Produktions-Steuerungs-System  
*Production Steering System*

<p align="left">
  <a href="https://github.com/Kush-Srigiri/PSS/blob/main/LICENSE">
    <img alt="Lizenz: MIT" src="https://img.shields.io/badge/license-MIT-blue.svg">
  </a>
  <img alt="Erstellt mit C#" src="https://img.shields.io/badge/made%20with-C%23-239120?logo=c-sharp&logoColor=fff">
  <img alt="Version" src="https://img.shields.io/github/v/release/Kush-Srigiri/PSS?include_prereleases">
</p>

> **PSS** ist dein smartes, modulares System zum **Planen, Steuern, Überwachen & Optimieren** von Produktionsprozessen.  
> Einfach. Flexibel. Zuverlässig.

---

## 📑 Inhaltsverzeichnis
1. [Funktionen](#funktionen)
2. [Architektur](#architektur)
3. [Technologie-Stack](#technologie-stack)
4. [Schnellstart](#schnellstart)
5. [Datenbankmodell](#datenbankmodell)
6. [Relationales Modell](#relationales-modell)
7. [Tests](#tests)
8. [Dokumentation](#dokumentation)
9. [Roadmap](#roadmap)
10. [Beitragen](#beitragen)
11. [Sicherheit](#sicherheit)
12. [Lizenz](#lizenz)
13. [Kontakt](#kontakt)

---
<br>

## Funktionen
- **Auftrags- & Jobplanung** – GANTT-basierte Planung mit endlicher Kapazität  
- **Live Shopfloor Monitoring** – Echtzeit-KPIs, OEE-Dashboards  
- **Adaptive Warnungen** – regelbasierte Benachrichtigungen bei Abweichungen  
- **Plug-in-API** – mit eigenen Datenquellen oder Logik erweiterbar  
- **Rollenbasierte Zugriffskontrolle** – feingranulare Benutzer- & Gruppenrechte  
- **REST + gRPC-Integrationsschicht** – Anbindung an ERP/MES, SPS & IIoT-Geräte  
- **Mandantenfähig** – Datenisolierung für Werke oder Kunden  

---
<br>

## Architektur 

<div style="display: flex; justify-content: center; gap: 40px;">
  <div style="text-align: center;">
    <h3>ER-Diagramm</h3>
    <img src="https://raw.githubusercontent.com/Kush-Srigiri/PSS/main/PSS_ER_Modell.png" alt="ER-Diagramm" style="max-width: 70%; height: auto;">
  </div>
  <div style="text-align: center;">
    <h3>Relationales Modell</h3>
    <img src="https://raw.githubusercontent.com/Kush-Srigiri/PSS/main/PSS_Relationale_Modell.png" alt="Relationales Modell" style="max-width: 70%; height: auto;">
  </div>
</div>

<br>

> Vollständige ER- & relationale Modelle findest du in  
> [`PSS_ER_Modell.drawio`](./PSS_ER_Modell.drawio) und [`PSS_Relationale_Modell.drawio`](./PSS_Relationale_Modell.drawio).
---

## Technologie-Stack
| Ebene  | Technologie |
|--------|-------------|
| Sprache | **C# 12**, .NET 8 LTS |
| Build & CI | GitHub Actions · `dotnet` CLI |
| Tests | xUnit · FluentAssertions |
| Doku | MkDocs Material |

---
<br>

## Schnellstart 
```bash
# 1 Klonen
git clone https://github.com/Kush-Srigiri/PSS.git
cd PSS

# 2 Starten
dotnet run
```


---
<br>


## Datenbankmodell

<p align="center">
  <img src="https://raw.githubusercontent.com/Kush-Srigiri/PSS/main/PSS_ER_Modell.png" alt="ER Diagramm">
</p>


*Bearbeitbares Diagramm siehe [`PSS_ER_Modell.drawio`](./PSS_ER_Modell.drawio).*

---
<br>

## Relationales Modell

<p align="center">
  <img src="https://raw.githubusercontent.com/Kush-Srigiri/PSS/main/PSS_Relationale_Modell.png" alt="Relational Model">
</p>


*Bearbeitbares Diagramm siehe [`PSS_Relationale_Modell.drawio`](./PSS_Relationale_Modell.drawio).*

---
<br>

## Tests 
```bash
dotnet test
```
Die Pipeline führt bei jedem Pull Request via GitHub Actions **Unit- und Integrationstests** aus und veröffentlicht die Testabdeckung im *Tests*-Badge.

---
<br>

## Dokumentation
Die komplette Doku liegt im Ordner **[`docs`](./docs)** und wird automatisch auf GitHub Pages veröffentlicht: <br>
<https://github.com/Kush-Srigiri/PSS/>.

---
<br>

## Roadmap
- [ ] Mehrsprachigkeit (EN/DE)  

---
<br>

## Beitragen
1. Fork → Feature Branch → PR  
2. Befolge die **Conventional Commits**-Spezifikation.  
3. Führe `dotnet format` aus, bevor du pushst.  
4. Schreibe Tests, die deine Änderung abdecken.

Lies [`CONTRIBUTING.md`](CONTRIBUTING.md) für die vollständige Anleitung.

<a href="https://github.com/kush-srigiri/pss/graphs/contributors">
  <img src="https://contrib.rocks/image?&columns=25&max=10000&&repo=kush-srigiri/pss" />
</a>

---
<br>

## Sicherheit
Wenn du eine Schwachstelle findest, wirf einen Blick in [`SECURITY.md`](SECURITY.md) und melde sie **verantwortungsvoll über die dort angegebene E-Mail**. 

---
<br>

## Lizenz
Veröffentlicht unter der **MIT-Lizenz**.  
Siehe [`LICENSE`](LICENSE) für Details.

---
<br>

## Kontakt
[**@Kush Srigiri**](https://github.com/Kush-Srigiri) – *Maintainer* 
[**@Jamie Poeffel**](https://github.com/Jamie-Poeffel) – *Maintainer* 
[**@Alessio Huber**](https://github.com/Alessio-Huber) – *Contributor* 

---

> © 2025 Kush Srigiri, Jamie Poeffel, Alessio Huber



