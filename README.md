# asp.net-core-api-crm

## run projecct
```
$ dotnet ef migration add YOUR_NAME
$ dotnet ef database drop
$ dotnet ef database update
```

and shortcutkey on vs code *Ctrl + F5*

## package install
```
<ItemGroup>
  <PackageReference Include="Microsoft.AspNetCore.App" />
  <PackageReference Include="Microsoft.AspNetCore.Razor.Design" Version="2.2.0" PrivateAssets="All" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="2.2.4" />
  <PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="2.2.4" />
  <PackageReference Include="Swashbuckle.AspNetCore" Version="4.0.1" />
  <PackageReference Include="System.Management" Version="4.5.0" />
</ItemGroup>
```

## Models
* Customers.cs
* Payments.cs
* TokenAccess.cs


  * Customers.cs:
  ```
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  namespace crm.Models
  {
      public class Customer
      {
          [Key]
          public long Id {get; set;}
          public string First_Name {get; set;}
          public string Last_Name {get; set;}
          public string Email {get; set;}
          public string Username {get; set;}
          public string Password {get; set;}
          public string Token {get; set;}
          public DateTime Created_At {get; set;}

          public bool Active {get; set;}

          public ICollection<Payment> payments {get; set;}
      }
  }
  ```
  
  * Payments.cs:
  ```
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  namespace crm.Models
  {
      public class Payment
      {
          [Key]
          public long Id {get; set;}
          public long CustomerId { get; set;}
          public string Key {get; set;}
          public int MaxUsed {get; set;}
          public double price {get; set;}
          public DateTime Expired_At {get; set;}
          public DateTime Created_At {get; set;}

          public ICollection<TokenAccess> TokenAccess {get; set;}
      }
  }
  ```
  
  * TokenAccess.cs:
  ```
  using System;
  using System.ComponentModel.DataAnnotations;

  namespace crm.Models
  {
      public class TokenAccess
      {
          [Key]
          public long Id {get; set;}
          public long PaymentId {get; set;}
          public string SerialNumber {get; set;}
          public string Key {get; set;}
          public DateTime Created_At {get; set;}
      }
  }
  ```
