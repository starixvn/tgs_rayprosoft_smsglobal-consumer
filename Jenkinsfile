pipeline {
	agent any
	
	environment {
		Registry = "registry.gitlab.com/rayprosoft/sms-system/"
	}
	
	parameters {
		gitParameter branchFilter: 'origin/(.*)', defaultValue: 'develop', name: 'BRANCH', type: 'PT_BRANCH'
	}

	stages {
		stage('Clone') {
			steps {
				git branch: "${params.BRANCH}", url: 'https://gitlab.com/rayprosoft/smsglobal-consumer.git' , credentialsId: 'gitlab-user-pwd'
			}
		}
	
		stage('Build') {
			steps {
				sh "dotnet build GlobalSmsConsumer/GlobalSmsConsumer.csproj"
			}
		}
		
		stage('Docker Image') {
			steps {
				script {
                    sh 'sudo dotnet build GlobalSmsConsumer/GlobalSmsConsumer.csproj -c:Release -t:ContainerBuild -p:DockerfileTag=registry.gitlab.com/rayprosoft/turnkey-gaming-system/smsglobal-consumer:dev'
					sh 'sudo docker push registry.gitlab.com/rayprosoft/turnkey-gaming-system/smsglobal-consumer:dev'
				}
			}
		}
	}
}