# Telefon Rehberi Projesi

Bu proje, ASP.NET Core MVC kullanılarak geliştirilmiş modern bir telefon rehberi uygulamasıdır. Kullanıcılar, kişileri ekleyebilir, düzenleyebilir, görüntüleyebilir ve silebilir. Ayrıca, kullanıcı arayüzü modal diyalog kutularını kullanarak kesintisiz bir deneyim sunar.

## Özellikler

- **Kişi Yönetimi**: Kişi ekleme, düzenleme, görüntüleme ve silme işlemleri
- **Modal Diyaloglar**: Sayfa yenileme gerekmeden form işlemleri için modern kullanıcı deneyimi
- **Veri Doğrulama**: Server-side ve client-side doğrulama ile kullanıcı girdilerinin kontrolü
- **Benzersizlik Kontrolleri**: Ad-Soyad kombinasyonu ve telefon numarası için benzersizlik kontrolü
- **Duyarlı Tasarım**: Bootstrap 5 ile her cihaza uyumlu arayüz
- **Veri Tablosu**: Gelişmiş sıralama, arama ve sayfalama özellikleriyle kişi listesi

## Kullanılan Teknolojiler

### Backend
- **Framework**: ASP.NET Core MVC 9.0
- **Veritabanı**: MySQL
- **ORM**: Entity Framework Core (Code First yaklaşımı)
- **Veritabanı Provider**: Pomelo.EntityFrameworkCore.MySql

### Frontend
- **Framework**: Bootstrap 5
- **JavaScript Kütüphanesi**: jQuery
- **Veri Tablosu**: DataTables
- **Modals**: Bootstrap Modal
- **Formlar**: jQuery Validation
- **Yazı Tipi**: Poppins font ailesi

## Proje Kurulumu

### Gereksinimler
- .NET 9.0 SDK veya üzeri
- MySQL Veritabanı
- Visual Studio 2025 / Visual Studio Code

### Kurulum Adımları

1. **Projeyi Klonlama**
   ```bash
   git clone https://github.com/username/telefon-rehberi-proje.git
   cd telefon-rehberi-proje
   ```

2. **Veritabanı Bağlantısı**
   
   `appsettings.json` dosyasındaki bağlantı cümlesini kendi MySQL sunucu bilgilerinize göre güncelleyin:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=TelefonRehberi;Uid=root;Pwd=yourpassword;"
   }
   ```

3. **Migration İşlemleri**
   
   Terminal veya Package Manager Console'dan aşağıdaki komutları çalıştırın:
   ```bash
   dotnet ef database update
   ```
   
   Veya Visual Studio'da:
   ```powershell
   Update-Database
   ```

4. **Projeyi Çalıştırma**
   
   Terminal'den:
   ```bash
   dotnet run
   ```
   
   Veya Visual Studio / VS Code üzerinden "Start" düğmesine basarak.

5. **Uygulamaya Erişim**
   
   Tarayıcınızdan aşağıdaki adresleri kullanarak uygulamaya erişebilirsiniz:
   - https://localhost:5001
   - http://localhost:5000

## Proje Yapısı

- **Controllers/**: MVC denetleyicileri
  - `HomeController.cs`: Ana sayfa denetleyicisi
  - `KisiController.cs`: Kişi işlemleri denetleyicisi

- **Models/**: Veri modelleri
  - `Kisi.cs`: Kişi veri modeli
  - `ErrorViewModel.cs`: Hata görünümü modeli

- **Views/**: Görünüm dosyaları
  - `Kisi/`: Kişi işlemleri görünümleri
    - `Index.cshtml`: Kişi listesi ana sayfası
    - `_CreatePartial.cshtml`: Kişi ekleme modalı
    - `_EditPartial.cshtml`: Kişi düzenleme modalı
    - `_DetailsPartial.cshtml`: Kişi detayları modalı
    - `_DeletePartial.cshtml`: Kişi silme modalı

- **Data/**: Veritabanı işlemleri
  - `AppDbContext.cs`: Entity Framework veritabanı bağlamı

- **wwwroot/**: Statik dosyalar
  - `css/`: CSS dosyaları
  - `js/`: JavaScript dosyaları
  - `lib/`: Kütüphane dosyaları

## Veritabanı Yapısı

### `Kisiler` Tablosu

| Alan | Tip | Açıklama |
| --- | --- | --- |
| KisiId | int | Birincil anahtar, otomatik artan |
| Ad | string | Kişinin adı (zorunlu) |
| Soyad | string | Kişinin soyadı (zorunlu) |
| TelefonNumarasi | string | 10 haneli telefon numarası (zorunlu) |

### Kısıtlamalar ve İndeksler

- **Benzersiz İndeksler**:
  - `IX_Kisiler_TelefonNumarasi`: Telefon numaralarının benzersiz olmasını sağlar
  - `IX_Kisiler_Ad_Soyad`: Ad ve soyadı kombinasyonunun benzersiz olmasını sağlar

- **Doğrulama Kuralları**:
  - Ad ve Soyad alanları boş bırakılamaz
  - TelefonNumarasi alanı 10 haneli rakamlardan oluşmalıdır

### ER Diyagramı

```
+----------------+
|     Kisiler    |
+----------------+
| KisiId (PK)    |
| Ad             |
| Soyad          |
| TelefonNumarasi|
+----------------+
```

## Yayına Alma (Deployment)

### Projeyi Yayın İçin Hazırlama

PowerShell terminal'den projeyi "publish" edin:
```powershell
dotnet publish -c Release -o ./publish
```

Bu komut, ./publish klasörüne production-ready dosyaları oluşturacaktır.

### Deployment Seçenekleri

#### 1. IIS (Windows Server)
- Windows Server'da .NET 9.0 Hosting Bundle'ı kurun
- IIS'te yeni bir uygulama havuzu (application pool) oluşturun
- Yeni bir website oluşturun ve fiziksel yolunu publish klasörüne yönlendirin
- web.config dosyasını kontrol edin ve gerekirse ayarlayın

#### 2. Linux (Nginx/Apache)
- Linux sunucuya .NET 9.0 Runtime yükleyin
- Nginx veya Apache'yi reverse proxy olarak yapılandırın
- Systemd service dosyası oluşturun (örn: telefon-rehberi.service)
- SSL sertifikası yapılandırın (Let's Encrypt)

Örnek Nginx yapılandırması:
```nginx
server {
    listen 80;
    server_name telefonrehberi.example.com;
    
    location / {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
    }
}
```

#### 3. Docker Konteyneri
Proje kök dizininde Dockerfile oluşturun:
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["TelefonRehberi.csproj", "."]
RUN dotnet restore "TelefonRehberi.csproj"
COPY . .
RUN dotnet build "TelefonRehberi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TelefonRehberi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TelefonRehberi.dll"]
```

Docker image oluşturma ve çalıştırma:
```bash
docker build -t telefon-rehberi:latest .
docker run -d -p 8080:80 --name telefon-rehberi-container telefon-rehberi:latest
```

#### 4. Cloud Servisleri (Azure, AWS, GCP)

##### Azure App Service
```bash
az webapp up --name TelefonRehberiApp --resource-group MyResourceGroup --sku F1 --location "West Europe"
```

##### AWS Elastic Beanstalk
AWS EB CLI kullanarak:
```bash
eb init TelefonRehberi --region eu-west-1 --platform dotnet
eb create telefon-rehberi-env
```

### Production Ortamı Güvenliği
- HTTPS/SSL sertifikası yapılandırın
- Hassas bilgileri User Secrets veya çevre değişkenlerinde saklayın
- Güvenlik başlıklarını (HSTS, CSP vb.) yapılandırın
- Firewall kurallarını ayarlayın (80/443 portları dışındakileri kapatın)

### Uygulamayı İzleme ve Bakım
- Application Insights veya Prometheus+Grafana gibi izleme araçları yapılandırın
- Log yönetimi için yapılandırma yapın (Serilog/NLog)
- Otomatik yedekleme stratejisi belirleyin
- Periyodik güvenlik güncelleştirmelerini planlayın

## Ekran Görüntüleri

[Buraya uygulama ekran görüntüleri eklenebilir]

## Lisans

Bu proje [MIT Lisansı](LICENSE) altında lisanslanmıştır.

## İletişim

İlgisoft Yazılım  
E-posta: info@ilgisoft.com  
Web: [www.ilgisoft.com](https://www.ilgisoft.com)
