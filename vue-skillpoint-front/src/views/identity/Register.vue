<template>
    <div class="container">

    <div class="row justify-content-center">
        <div class="col-md-4">
        <h1>Register</h1>
            <div v-if="errorMsg != null" class="text-danger validation-summary-errors" data-valmsg-summary="true">
                <ul>
                    <li>{{ errorMsg }}</li>
                </ul>
            </div>

            <div>
                <div class="form-group">
                    <label class="control-label" for="FirstName">Email</label>
                    <input v-model="email" class="form-control" type="text" />
                </div>
                <div class="form-group">
                    <label class="control-label" for="LastName">Password</label>
                    <input v-model="password" class="form-control" type="password" />
                </div>
                <div class="form-group mt-3">
                    <button @click="registerClicked()" type="submit"  class="btn btn-primary">Login </button>
                </div>
            </div>
        </div>
    </div>
    </div>
    

</template>

<script lang="ts">
import { IdentityService } from "@/services/IdentityService";
import { useIdentityStore } from "@/stores/identity";
import { Options, Vue } from "vue-class-component";
import { RouterLink } from "vue-router";

@Options({
    components: {
    },
    props: {},
    emits: [],
})
export default class Register extends Vue {
    identityStore = useIdentityStore();
    
    email: string = '';
    password: string = '';
    errorMsg: string | null = null;


    identityService = new IdentityService();

        mounted(){
        if (this.identityStore.$state.jwt != null ) {
            this.$router.push("/");
        }   
    }

    async registerClicked(): Promise<void> {
        var res = await this.identityService.register(this.email, this.password);
        if (res.status > 300) {
            this.errorMsg = res.errorMsg ?? "Unexprected error";
            console.log(res.errorMsg);
        }else {
            this.identityStore.$state.jwt = res.data!;
            this.$router.push("/");
        }

    }

}
</script>

