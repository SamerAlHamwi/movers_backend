node {
  stage('SCM') {
    checkout scm
  }
  stage('SonarQube Analysis') {
    def scannerHome = tool 'sonar-dotnet'
    withSonarQubeEnv() {
      sh "dotnet /var/jenkins_home/SonarScanner.MSBuild.dll begin /k:\"movaror-backend\""
      sh "dotnet build"
      sh "dotnet /var/jenkins_home/SonarScanner.MSBuild.dll end"
    }
  }
}
