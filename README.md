# Xautimation

Xautimation projesi, Selenium ve C# kullanılarak Twitter otomasyon işlemlerini gerçekleştirmek için geliştirilmiştir.

## Gereksinimler

Projenin düzgün çalışabilmesi için aşağıdaki gereksinimlere ihtiyaç vardır:

- **Visual Studio:** Proje, Visual Studio IDE üzerinde geliştirilmiştir. [Visual Studio İndirme Sayfası](https://visualstudio.microsoft.com/tr/downloads/)
- **NuGet Paketleri:**
  - Selenium WebDriver: `Install-Package Selenium.WebDriver`
  - Selenium Chrome Driver: `Install-Package Selenium.Chrome.WebDriver`

## Kurulum

1. Projeyi klonlayın veya indirin.
2. Visual Studio'yu açın.
3. `TwitterOtomasyon.sln` dosyasını açın.
4. Gerekli NuGet paketlerini yükleyin.

## Chromedriver Kurulumu

Proje, ChromeDriver kullanarak tarayıcıyı kontrol eder. Projenin çalışabilmesi için ChromeDriver'ı indirip proje klasörünün içine ekleyin:

1. [ChromeDriver İndirme Sayfası](https://sites.google.com/chromium.org/driver/) adresine gidin.
2. İşletim sistemine ve chrome sürümünüze uygun olan sürümü indirin.
3. İndirdiğiniz dosyayı proje klasörüne kopyalayın veya proje içinde bir klasör oluşturup oraya yerleştirin.

Örneğin, `debug` klasörüne ChromeDriver'ı eklemek için:

```plaintext
Xautimation
│   TwitterOtomasyon.sln
│
└───bin
│   └───Debug
│       └───ChromeDriver.exe

![WhatsApp Görsel 2024-02-04 saat 16 51 39_48bbd18d](https://github.com/barangulmus/Xautomation/assets/149194958/c48ed5b5-8330-4822-a0fa-7497b4bd0cfb)
![WhatsApp Görsel 2024-02-04 saat 17 10 11_920eef41](https://github.com/barangulmus/Xautomation/assets/149194958/4864079d-0990-4b69-b155-e8eaba1e2fc1)
