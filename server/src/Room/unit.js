class Unit {


  //<editor-fold> Direction Description
  /*
          STOP = 0,
          RIGHT = 1,
          LEFT = 2,
          UP = 3,
          DOWN = 4
  */
  //</editor-fold>

//<editor-fold> Unit Type Description
/*
  0 = Worker
  2 = Archer
  1 = Swordman
  3 = Lancer
*/
//</editor-fold>

//<editor-fold> State
/*
  0 = move/normal
  1 = attacking
  2 = running
*/
//</editor-fold>

  constructor(type,owner,board){
    this.type = type
    this.direction = 0
    this.owner = owner
    this.state = 0
    this.board = board
    this.extraDefense = 0
    this.assignPower()
    this.attackLoop = setInterval(this.checkAttackRange,this.speed*750,this)
  }

  assignPower(){
    if (this.type==0){
      this.attack = 1
      this.speed = 3
      this.range = 1
      this.hp = 6
      this.atkSpd = 6
    }else if (this.type == 3){
      this.attack = 2
      this.speed = 2
      this.range = 2
      this.hp = 7
      this.atkSpd = 1
    }else if (this.type == 1){
      this.attack = 3
      this.speed = 1
      this.range = 1
      this.hp = 10
      this.atkSpd = 2
    }else if (this.type == 2){
      this.attack = 5
      this.speed = 3
      this.range = 1
      this.hp = 15
      this.atkSpd = 3
    }
  }

  hide(){
    this.isHide = !this.isHide
  }

  getDirection(remote){
    if (this.owner.playerNum != remote.playerNum){
      if (this.owner.playerNum == 0) return this.inverseDirection(this.direction)
      return this.direction
    }
    if (this.owner.playerNum == 0) return this.direction
    return this.inverseDirection(this.direction)
  }

  inverseDirection(direction){
    if (direction==1) return 2
    else if (direction==2) return 1
    else if (direction==3) return 4
    else if (direction==4) return 3
  }

  isOwner(remote) {
    return this.owner == remote
  }

  capture(unit){
    this.target = unit
    this.state = 1
    this.attackTask = setInterval(attack(this.target),this.atkSpd*500)
  }

  damage(unit){
    this.board.getUnit(this.x,this.y,this.x,this.y,1)
    unit.defense(this.attack)
  }

  defense(attack){
    let damage = attack-this.extraDefense
    this.hp -= damage
    if (this.isDead()){
      this.board.getUnit(this.x,this.y,this.x,this.y,2)
      this.board.units[this.x][this.y] = null
      clearInterval(this.attackLoop)
      delete this
    }else {
      this.board.getUnit(this.x,this.y,this.x,this.y,0)
    }
  }

  isDead(){
    return this.hp<=0
  }

  setPosition(x,y){
    this.x = x
    this.y = y
  }

  checkAttackRange(self){
    if (self.state == 0){
      for (var x = self.x-self.range;x<=self.x+self.range;x++){
        for (var y = self.y-self.range;y<=self.y+self.range;y++){
          if (x>=0&&y>=0&&x<16&&y<16){
            if (self.board.units[x][y]&&self.board.units[x][y]!=self){
              if (self.board.units[x][y].state!=2&&self.board.units[x][y].owner != self.owner){
                self.target = self.board.units[x][y]
                self.board.units[x][y].direction = 0
                self.direction = 0
                self.state = 1
                self.board.getUnit(self.x,self.y,self.x,self.y,0)
                self.board.getUnit(self.board.units[x][y].x,self.board.units[x][y].y,self.board.units[x][y].x,self.board.units[x][y].y,0)
                break
              }
            }
            if (self.target) break
          }
        }
      }
    }
    if (self.state == 1){
      if (self.target.x<=self.x+self.range&&self.target.x>=self.x-self.range&&self.target.y<=self.y+self.range&&self.target.y>=self.y-self.range){
        if (!self.target.isDead()){
          self.damage(self.target)
        }else {
          this.target = null
          self.state = 0
        }
      }else {
        self.target = null
        self.state = 0
      }
      self.board.getUnit(self.x,self.y,self.x,self.y,0)
    }
  }
}

module.exports = Unit
