resource "azurerm_resource_group" "rg" {
  name     = "infra-utvm"
  location = "East US 2"
}

resource "azurerm_container_app_environment" "utvm_container_app_env" {
  name                = "utvm-container-app-env"
  resource_group_name = azurerm_resource_group.rg.name
  location            = "East US 2"
}