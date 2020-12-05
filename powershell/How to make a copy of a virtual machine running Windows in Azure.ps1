<#
ref: https://blog.kloud.com.au/2016/10/06/copy-vm-running-windows-in-azure/

	Create a new virtual machine from a copy of the disks of another

Having finalized the configuration of the source virtual machine the steps required are as follows.

Stop the source virtual machine, then using Storage Explorer copy it’s disks to a new location and rename them in line with the target name of the new virtual machine.
Run the following in PowerShell making the required configuration changes.


#>

Login-AzureRmAccount
Get-AzureRmSubscription –SubscriptionName "<subscription-name>" | Select-AzureRmSubscription
 
$location = (get-azurermlocation | out-gridview -passthru).location
$rgName = "<resource-group>"
$vmName = "<vm-name>"
$nicname = "<nic-name>"
$subnetID = "<subnetID>"
$datadisksize = "<sizeinGB>"
$vmsize = (Get-AzureLocation | Where-Object { $_.name -eq "East US"}).VirtualMachineRoleSizes | out-gridview -passthru
$osDiskUri = "https://<storage-acccount>.blob.core.windows.net/vhds/<os-disk-name.vhd>"
$dataDiskUri = "https://<storage-acccount>.blob.core.windows.net/vhds/<data-disk-name.vhd>"

<#
Notes: The URIs above belong to the copies not the original disks and the SubnetID refers to it’s resource ID.
#>

$nic = New-AzureRmNetworkInterface -Name $nicname -ResourceGroupName $rgName -Location $location -SubnetId $subnetID
$vmConfig = New-AzureRmVMConfig -VMName $vmName -VMSize $vmsize
$vm = Add-AzureRmVMNetworkInterface -VM $vmConfig -Id $nic.Id
$osDiskName = $vmName + "os-disk"
$vm = Set-AzureRmVMOSDisk -VM $vm -Name $osDiskName -VhdUri $osDiskUri -CreateOption attach -Windows
$dataDiskName = $vmName + "data-disk"
$vm = Add-AzureRmVMDataDisk -VM $vm -Name $dataDiskName -VhdUri $dataDiskUri -Lun 0 -Caching 'none' -DiskSizeInGB $datadisksize -CreateOption attach
New-AzureRmVM -ResourceGroupName $rgName -Location $location -VM $vm

<#
List virtual machines in a resource group.
#>

$vmList = Get-AzureRmVM -ResourceGroupName $rgName
$vmList.Name

