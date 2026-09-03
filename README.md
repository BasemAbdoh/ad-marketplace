# Ad Marketplace — .NET 8 + Angular 18

نظام سوق إعلانات مقسّم إلى `backend/` (API بثلاث طبقات) و`frontend/` (Angular Standalone/Lazy Loading). يحتوي مخطط SQL على الجداول الثلاثين المطلوبة، ولا ينشئ القيد المكرر `FK_Businesses_Regions1`.

## البنية

- **AdDatabase.API**: Controllers، DTOs، AutoMapper، JWT، Swagger، Serilog، Exception Middleware، SignalR ورفع الصور.
- **AdDatabase.BLL**: كيانات المجال والعقود وواجهات المستودعات والخدمات ومنطق العمل. توجد كيانات المجال في BLL عمداً حتى تعتمد DAL على طبقة الأعمال وليس العكس (Dependency Inversion).
- **AdDatabase.DAL**: EF Core `ApplicationDbContext`، المستودع العام، مستودعات Ads/Conversations المحسنة وUnit of Work.
- **frontend**: Standalone Components، lazy routes، RxJS/BehaviorSubject، Reactive Forms، Bootstrap 5، JWT interceptor، guards وSignalR.

## المتطلبات

- Docker Desktop/Compose، أو .NET SDK 8 + SQL Server 2022 + Node.js 20 أو أحدث.
- لا تستخدم أسرار `.env.example` في الإنتاج.

## التشغيل السريع

```bash
cp .env.example .env
# غيّر SQL_PASSWORD وJWT_SECRET أولاً
docker compose up --build
```

يتم انتظار SQL Server ثم تشغيل `backend/database/init.sql` **قبل** الـ API. السكربت قابل لإعادة التشغيل ويتوقف إذا كان المخطط موجوداً. العناوين:

- API: `http://localhost:8080`
- Swagger: `http://localhost:8080/swagger`
- Health: `http://localhost:8080/health`

ثم شغّل الواجهة:

```bash
cd frontend
npm ci
npm start
```

الواجهة: `http://localhost:4200`.

## التشغيل المحلي دون Docker للـ API

1. شغّل SQL Server ونفّذ `backend/database/init.sql` بواسطة `sqlcmd`/SSMS.
2. اضبط الأسرار (لا تخزنها في Git):

```bash
cd backend
export ConnectionStrings__DefaultConnection='Server=localhost,1433;Database=AdDatabase;User Id=sa;Password=...;TrustServerCertificate=True'
export Jwt__Secret="$(openssl rand -base64 48)"
dotnet restore
dotnet run --project AdDatabase.API
```

## متغيرات البيئة

| المتغير | الوصف |
|---|---|
| `ConnectionStrings__DefaultConnection` | اتصال SQL Server |
| `Jwt__Secret` | سر عشوائي لا يقل عن 32 بايت |
| `Jwt__Issuer`, `Jwt__Audience` | مصدر/جمهور JWT |
| `FrontendUrl` | أصل Angular المسموح في CORS |
| `SQL_PASSWORD`, `JWT_SECRET` | اختصارات Docker Compose |

غيّر `frontend/src/environments/environment.ts` إذا تغير عنوان API. في الإنتاج استخدم HTTPS، مخزن أسرار، object storage للصور، reverse proxy، rate limiting، فحص malware للمرفقات ونسخاً احتياطية.

## المستخدم الإداري

ينشئ السكربت دورَي `Admin` و`User` فقط. سجّل مستخدماً ثم رقّه بطريقة مضبوطة من قاعدة البيانات:

```sql
USE AdDatabase;
UPDATE Users SET RoleId=(SELECT Id FROM Roles WHERE Name='Admin') WHERE Email='admin@example.com';
```

سجّل الخروج ثم الدخول لتضمين الدور الجديد في JWT.

## أهم المسارات

جميع الاستجابات بشكل `{ success, message, data }`. مسارات الكتابة محمية بـ JWT:

- `/api/auth/register`, `/api/auth/login`, `/api/auth/profile`
- CRUD في `/api/ads` مع pagination/filtering و`POST /api/uploads/images`
- `/api/categories`, `/api/comments`, `/api/favorites`
- `/api/conversations/*` وSignalR في `/hubs/chat`
- `/api/users/{id}/rating`
- `/api/admin/users`, `/api/admin/reports`, `/api/admin/ads/{id}/status`

## فحوص البناء

```bash
cd backend && dotnet build AdDatabase.sln -c Release
cd frontend && npm ci && npm run build
```

> السكربت المرفق في الطلب لم يكن متاحاً كملف داخل بيئة التنفيذ؛ لذلك أُنشئ `init.sql` مطابقاً لوصف الجداول الثلاثين. إذا كان السكربت الأصلي يحتوي أسماء أعمدة مختلفة، قارنها مع `ApplicationDbContext` قبل ترحيل بيانات حقيقية.
