/*
Developed github.com/bsBilal
*/
var TIME_LIMIT=10;
var SLEEP_TIME=5;

function User(id,money,betValue)
{
	this.id=id;
	this.money=money;
	this.betValue=betValue;
}
var ArrayList=require('arrayList')
var app=require('express')();
var http=require('http').Server(app);
var io=require('socket.io')(http);

//create an list of user playing
var listUsers=new ArrayList;
var money=0;

//create server

app.get('/',function(req,res){

	res.sendFile('index.html',{root: __dirname})
});

function getRandomInt(max){

	return Math.floor(Math.random() * Math.floor(max));
	}
	
	function sleep(sec)
	{
		return new Promise(resolve=>setTimeout(resolve,sec*1000));
		
	}
	
	async function countDown() //countdown for all users
	{
		var timeTotal=TIME_LIMIT;
		do{
			io.sockets.emit('broadcast',timeTotal);
			timeTotal--;
			await sleep(1);
		}while(timeTotal>0)
		
		//time has finished
		processResult(); //sendmoney
		
		//reset game for next turn
		
		timerTotal=TIME_LIMIT;
		money=0;
		io.sockets.emit('wait_before_restart',SLEEP_TIME);//send messagfe and waitrs
		io.sockets.emit('money_send',0);
		await sleep(SLEEP_TIME);
		
		io.sockets.emit('restart',1);
		
		countDown();
	}
	
function processResult()
{
	console.log('server is processing data');
	var result=getRandomInt(2);
	console.log('Lucky number : '+ result); 
	io.sockets.emit('result',result);
	
	listUsers.unique();
	
	//show how many user won
	var count=listUsers.find(function(user){
		
		return user.betValue==result;
	}).length;
	
	listUsers.find(function(user){
		if(user.betValue==result)
			io.to(user.id).emit('reward',parseInt(user.money)*2);
		else
			io.to(user.id).emit('lose',user.money);
	});
	
	console.log('\x1b[32m','we have '+count+' people(s) are winner');
	
	listUsers.clear();

}

//connect process to socket

io.on('connection',function(socket){

	console.log('A new user '+socket.id+' has connected');
	
	io.sockets.emit('money_send',money);
	
	socket.on('client_send_money',function(objectClient)
	{
		console.log(objectClient);
		var user=new User(socket.id,objectClient.money,objectClient.betValue);
		
		console.log('we receive '+user.money+' from '+ user.id);
		console.log('User: '+user.id+' bet value '+user.betValue);
		
		money+=parseInt(user.money);
		
		console.log('=x1b[42m','Sum of money: '+money);//update on our server
		
		
		//save user list user online
		listUsers.add(user);
		console.log('total online users: '+listUsers.length);
		
		//send update money to all users
		io.sockets.emit('money_send',money);
	});
	
	//when someone dc
socket.on('disconnect',function(socket){
	console.log('User '+socket.id+' has leaved');
});

});


//start server
http.listen(3000,function(){
	console.log('server game has started on port 3000');
	
	countDown();
	});






	


