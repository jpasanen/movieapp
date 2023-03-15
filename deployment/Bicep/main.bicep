@description('Object with general settings for all of the environments. (Eg. Location)')
param global object
@description('Synapse SQL Administrator password')
@secure()
param synapseSqlAdministratorLoginPassword string
@description('Synapse Data Lake Account URL')
param synapseDataLakeAccountUrl string
@description('Build ID from Azure DevOps')
param buildId string = utcNow()
@description('ID for Azure AD Admins group')
param aadAdminGroupId string

var namingPrefix = '<replace>'
var shortEnv = {
  Demo: 'demo'
}

var naming = {
  // Data Lake
  dataLake: '${namingPrefix}dls${shortEnv[global.environment]}01'

  // Synapse
  synapseWorkspace: '${namingPrefix}-syn-${shortEnv[global.environment]}-01'

  // Data lake containers
  bronzeContainer: 'bronze'
  sourceContainer: 'source'
}

// Synapse Workspace
module synapse 'synapse.bicep' = {
  name: 'syn-${buildId}'
  params: {
    global: global
    dataLakeName: naming.dataLake
    synapseDataLakeAccountUrl: synapseDataLakeAccountUrl
    aadAdminGroupId: aadAdminGroupId
    sqlAdministratorLoginPassword: synapseSqlAdministratorLoginPassword
    synapseWorkspace: naming.synapseWorkspace
  }
}

// Data Lake storage
module dataLake 'dataLake.bicep' = {
  name: 'adls-${buildId}'
  params: {
    dataLakeName: naming.dataLake
    global: global
    bronzeContainer: naming.bronzeContainer
    sourceContainer: naming.sourceContainer
  }
}

output synapseWorkspaceName string = synapse.outputs.synapseWorkspaceName
output synapseWorkspaceResourceId string = synapse.outputs.synapseWorkspaceResourceId
output developerGroupId string = global.developerGroupId
output subscriptionId string = subscription().id
output datalakeName string = dataLake.outputs.dataLakeName
