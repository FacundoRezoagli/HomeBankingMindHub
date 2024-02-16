var app = new Vue({
    el:"#app",
    data:{
        clientInfo: {},
        //error: null
        errorToats: null,
        errorMsg: null,
    },
    methods:{
        getData: function(){
            //axios.get("/api/clients/1")
            axios.get("/api/clients/current")
            .then(function (response) {
                //get client ifo
                app.clientInfo = response.data;
            })
            .catch((error) => {
                this.errorMsg = error.response.data;
                this.errorToats.show();
            })
        },
        formatDate: function(date){
            return new Date(date).toLocaleDateString('en-gb');
        },
        signOut: function () {
            axios.post('/api/auth/logout')
                .then(response => window.location.href = "/index.html")
                .catch((error) => {
                    this.errorMsg = error.response.data;
                    this.errorToats.show();
                })
        },
        create: function(){
            axios.post('/api/clients/current/accounts')
            .then(response => window.location.reload())
            .catch((error) =>{
                this.errorMsg = error.response.data;  
                this.errorToats.show();
            })
        }        
    },
    mounted: function () {
        this.errorToats = new bootstrap.Toast(document.getElementById('danger-toast'));
        this.getData();
    }
})