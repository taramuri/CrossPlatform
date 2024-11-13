# -*- mode: ruby -*-
# vi: set ft=ruby :

# Define the host IP addresses
hosts = {
  "ubuntu" => "192.168.56.10",
  "windows" => "192.168.56.11"
}

Vagrant.configure("2") do |config|
  # Common network configuration
  config.vm.network "public_network", bridge: "default"
  
  # BaGet URL as an environment variable for flexibility
  baget_url = "http://192.168.56.1:8080/v3/index.json"
  
  # Ubuntu Machine Configuration
  config.vm.define "ubuntu" do |ubuntu|
    ubuntu.vm.box = "bento/ubuntu-22.04"
    ubuntu.vm.hostname = "ubuntu-vm"
    ubuntu.vm.network "forwarded_port", guest: 7252, host: 7252
    ubuntu.vm.network "private_network", ip: hosts["ubuntu"]
    
    ubuntu.vm.provider "virtualbox" do |v|
      v.name = "Ubuntu VM"
      v.memory = "3072"
      v.cpus = 2
    end
    
    ubuntu.vm.synced_folder ".", "/home/vagrant/project"
    ubuntu.vm.provision "shell", path: "provision-ubuntu.sh", env: { BAGET_URL: baget_url }
  end

  # Windows Machine Configuration
  config.vm.define "windows" do |windows|
    windows.vm.box = "gusztavvargadr/windows-10"
    windows.vm.hostname = "windows-vm"
    windows.vm.network "private_network", ip: hosts["windows"]
    
    windows.vm.provider "virtualbox" do |v|
      v.name = "Windows VM"
      v.memory = "3072"
      v.cpus = 2
    end
    
    windows.vm.synced_folder ".", "C:/project"
    windows.vm.provision "shell", path: "provision-windows.sh", env: { BAGET_URL: baget_url }
  end
end