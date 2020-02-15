using System.Text.Json;
using Amazon.CDK;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.SSM;

namespace ParameterStoreDemo.Infrastructure
{
    public class ParameterStoreInfrastructureStack : Stack
    {
        internal ParameterStoreInfrastructureStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var lambda = new Function(this,"ParameterStoreFunction", new FunctionProps()
            {
                Runtime = Runtime.DOTNET_CORE_2_1,
                Timeout = Duration.Minutes(1),
                MemorySize = 128,
                Handler = "ParameterStoreDemo.Lambda::ParameterStoreDemo.Lambda.Function::FunctionHandler",
                Code = Code.FromAsset("../ParameterStore.Lambda/bin/Release/netcoreapp2.1/publish"),
                FunctionName = "ParameterStoreDemo"
            });

            var role = lambda.Role;
            
            
           
            var firstName = new StringParameter(this, "FirstNameParameter", new StringParameterProps()
            {
                ParameterName = "/parameterstoredemo/firstname",
                StringValue = "Donald"
            });
            
            var lastName = new StringParameter(this, "LastNameParameter", new StringParameterProps()
            {
                ParameterName = "/parameterstoredemo/lastname",
                StringValue = "Trump"
            });
            
            var birthdate = new StringParameter(this, "birthdate", new StringParameterProps()
            {
                ParameterName = "/parameterstoredemo/birthdate",
                StringValue = "06/14/1946"
            });
            
            var configuration = new StringParameter(this, "ConfigurationParameter", new StringParameterProps()
            {
                ParameterName = "/parameterstoredemo",
                StringValue = "This is a list of configuration for Parameter Store Demo app"
            });
            
            firstName.GrantRead(role);
            lastName.GrantRead(role);
            birthdate.GrantRead(role);
            configuration.GrantRead(role);
            
            var statement = new PolicyStatement(new PolicyStatementProps()
            {
                Resources = new [] { configuration.ParameterArn},
                Effect = Effect.ALLOW,
                Actions = new [] { "ssm:GetParametersByPath" }
            });
            
            role.AddToPolicy(statement);

        }
    }
}
