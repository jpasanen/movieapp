param dataLakeName string
param identityid string
// Storage Blob Data Reader role
param roleDefinitionId string = subscriptionResourceId('Microsoft.Authorization/roleDefinitions', 'ba92f5b4-2d11-453d-a403-e96b0029c9fe')

resource datalake 'Microsoft.Storage/storageAccounts@2019-06-01' existing = {
  name: dataLakeName
}

resource roleAssignment 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
  scope: datalake
  name: guid(datalake.id, identityid, roleDefinitionId)
  properties: {
    roleDefinitionId: roleDefinitionId
    principalId: identityid
  }
}
