#!/bin/bash
NAME=$1
dotnet new console -n $NAME -o $NAME
mv $NAME/Program.cs $NAME/programa.cs
sed -i 's/<\/PropertyGroup>/<RootNamespace>POO<\/RootNamespace>\n    <\/PropertyGroup>/' $NAME/$NAME.csproj
cat > $NAME/programa.cs << 'EOF'
namespace POO;

class Program
{
    static void Main(string[] args)
    {

    }
}
EOF
dotnet sln POO.sln add $NAME/$NAME.csproj