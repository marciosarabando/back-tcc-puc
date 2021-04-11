dotnet ef database drop --startup-project TCC.INSPECAO.Api/

rm -r TCC.INSPECAO.Infra/Migrations

cd TCC.INSPECAO.Infra

dotnet ef migrations add InitialCreate --startup-project ../TCC.INSPECAO.Api/

dotnet ef database update --startup-project ../TCC.INSPECAO.Api/

cd ..