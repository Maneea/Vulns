import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import '@/globals.css'
import VueGridLayout from 'vue-grid-layout'

createApp(App).use(VueGridLayout).use(store).use(router).mount('#app')
