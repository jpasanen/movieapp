@description('Object with general settings for all of the environments. (Eg. Location)')
param global object
@description('Data Lake name')
param dataLakeName string
@description('Synapse Data Lake Account URL')
param synapseDataLakeAccountUrl string
@description('ID for Azure AD Admins group')
param aadAdminGroupId string
@description('Synapse SQL Administrator password')
@secure()
param sqlAdministratorLoginPassword string
@description('Synapse workspace name')
param synapseWorkspace string

param buildId string = utcNow()

resource synapse 'Microsoft.Synapse/workspaces@2021-06-01' = {
  name: synapseWorkspace
  location: global.location
  tags: {
    Owner: '<replace>'
  }
  properties: {
    azureADOnlyAuthentication: false
    publicNetworkAccess: 'Enabled'
    managedVirtualNetwork: 'default'
    sqlAdministratorLogin: 'sqladminuser'
    sqlAdministratorLoginPassword: sqlAdministratorLoginPassword
    trustedServiceBypassEnabled: false
    cspWorkspaceAdminProperties: {
      initialWorkspaceAdminObjectId: global.developerGroupId
    }
    defaultDataLakeStorage: {
      resourceId: resourceId('Microsoft.Storage/storageAccounts', dataLakeName)
      accountUrl: synapseDataLakeAccountUrl
      filesystem: 'synapse'
      createManagedPrivateEndpoint: true
    }
  }
  identity: {
    type: 'SystemAssigned'
  }
}

resource firewallRule 'Microsoft.Synapse/workspaces/firewallRules@2021-06-01' = {
  name: 'allowAll'
  parent: synapse
  properties: {
    startIpAddress: '0.0.0.0'
    endIpAddress: '255.255.255.255'
  }
}

resource aadAdmin 'Microsoft.Synapse/workspaces/administrators@2021-06-01' = {
  name: 'activeDirectory'
  parent: synapse
  properties: {
    administratorType: 'ActiveDirectory'
    sid: aadAdminGroupId
    tenantId: subscription().tenantId
  }
}

module roleAssignment 'roleAssignment.bicep' = {
  name: 'synapse-roleAssignment-${buildId}'
  params: {
    dataLakeName: dataLakeName
    identityid: synapse.identity.principalId
  }
}

output synapseWorkspaceName string = synapse.name
output synapseWorkspaceResourceId string = synapse.id
