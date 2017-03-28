class Lobby {
  constructor(){
    this.remotes = []
    this.room = []
  }

  addRemote(remote){
    this.remotes.push(remote)
  }

  removeRemote(remote){
    this.remotes.splice(this.remotes.indexOf(remote), 1)
  }

}

module.exports = Lobby
