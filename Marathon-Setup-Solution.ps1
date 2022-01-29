# Configuration
$solutionBasePath = "C:\Users\Kyle\Code\"
$solutionName = "Marathon"
$solutionFullPath = $solutionBasePath + $solutionName

# Test for existence
if (Test-Path $solutionFullPath) {
    echo "Solution folder already exists: $solutionName"
}
else {
    # Make project folder
    cd $solutionBasePath
    mkdir $solutionName
    cd $solutionName
    
    # Git Init
    git init
    
    # Copy this script to repo
    copy $PSCommandPath $solutionFullPath
    git add .
    git commit -m "Copy setup script"

    # Create solution
    dotnet new sln -n $solutionName
    git add .
    git commit -m "Added new solution"

    # Create HangFire project
    $projectName = "HangFire"
    $projectPath = "$solutionName.$projectName"
    dotnet new console -o .\$projectPath\ -n "$projectPath"
    dotnet sln add .\$projectPath\$projectPath.csproj
    cd .\$projectPath
    git add .
    git commit -m "Added $projectName project"
    dotnet add package HangFire.Core
    dotnet add package HangFire.SQLServer
    git add .
    git commit -m "Added $projectName packages"
      
    # Create unit tests project
    cd ..
    $projectName = "UnitTests"
    $projectPath = "$solutionName.$projectName"
    dotnet new mstest -o .\$projectPath\ -n "$projectPath"
    dotnet sln add .\$projectPath\$projectPath.csproj
    cd .\$projectPath
    git add .
    git commit -m "Added $projectName project"
    
    # # Force open to auto generate remaining changes
    # code .
    
    # # Final commit (wait for generation to complete)
    # sleep 30 
    # git add .
    # git commit -m "Autogenerated init files"
    
}
