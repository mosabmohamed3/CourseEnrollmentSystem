# Course Enrollment System

نظام تسجيل مقررات مبني بـ ASP.NET Core MVC و Entity Framework Core (قاعدة بيانات InMemory) مع طبقة خدمات لعزل منطق الأعمال.

## المزايا
- إدارة الطلاب: إضافة، تعديل، حذف، عرض.
- إدارة المقررات: إضافة، تعديل، حذف، عرض، مع ترقيم صفحات.
- التسجيل في المقررات مع التحقق من السعة ومنع التكرار.
- عرض المقاعد المتاحة في نموذج التسجيل ديناميكياً باستخدام jQuery.
- بنية طبقية (Controllers خفيفة، Services لكل من Student/Course/Enrollment).
- بيانات أولية للطلاب والمقررات.

## المتطلبات
- .NET 8 SDK

## التشغيل
```bash
dotnet run
```
ثم افتح المتصفح على `https://localhost:5001` أو `http://localhost:5000`.

## البنية
- `Data/`: `AppDbContext` وإعدادات النموذج والبيانات الأولية.
- `Models/`: الكيانات (Student, Course, Enrollment).
- `Services/Interfaces` و `Services/Implementations`: منطق الأعمال.
- `Controllers/`: يستدعي الخدمات فقط.
- `ViewModels/`: نماذج العرض (مثل CourseListViewModel, EnrollmentFormViewModel).
- `Views/`: واجهة المستخدم مع partial views لعرض الطلاب والمقررات.

## ملاحظات
- قاعدة البيانات InMemory يتم تهيئتها عند التشغيل (`EnsureCreated`)، وتفقد البيانات عند إيقاف التطبيق.

